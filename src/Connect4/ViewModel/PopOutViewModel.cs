using Connect4.BL.Services;
using Connect4.BL.Services.GridServices;
using Connect4.Commands;
using Connect4.DAL.DatabaseHelpers;
using Connect4.DAL.DataModels;
using Connect4.ViewModel.Interfaces;
using Connect4.Views.PopUps;
using Connect4.ViewUserControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using static Connect4.ViewModel.PopOutViewModel;
using NavService = Connect4.Services.NavigationService;

namespace Connect4.ViewModel
{
    public class PopOutViewModel : ILoadUser, INotifyPropertyChanged
    {
        public User CurrentUser { get; set; }

        private readonly NavService _navigationService;
        private GridPopService _gridService;
        private readonly UserService _userService;
        private readonly UserCustomizableService _userCustomizableService;
        private readonly UserAchievementService _userAchievementService;

        public Grid MainGrid;
        public Grid GameGrid;
        public Canvas GameCanvas;

        public ICommand NavigateToPickVariantCommand { get; private set; }
        public ICommand DropBallCommand { get; private set; }

        public ICommand ToggleModeCommand { get; private set; }

        private readonly string _player1Turn = "Player1 Turn";
        private readonly string _player2Turn = "Player2 Turn";
        private string _playerTurn;

        private bool _isPopMode;

        public bool IsPopMode
        {
            get { return _isPopMode; }
            set
            {
                _isPopMode = value;
                OnPropertyChanged(nameof(IsPopMode));
                OnPropertyChanged(nameof(IsPutMode));
            }
        }

        public bool IsPutMode => !_isPopMode;

        public string PlayerTurn
        {
            get { return _playerTurn; }
            set
            {
                if (_playerTurn != value)
                {
                    _playerTurn = value;
                    OnPropertyChanged("PlayerTurn");
                }
            }
        }

        private string token1 = "/Resources/Images/Tokens/TokenBlue.png";
        private string token2 = "/Resources/Images/Tokens/TokenRed.png";
        private string _tokenSkin = "/Resources/Images/Tokens/TokenBlue.png";

        public string TokenSkin
        {
            get { return _tokenSkin; }
            set
            {
                if (_tokenSkin != value)
                {
                    _tokenSkin = value;
                    OnPropertyChanged("TokenSkin");
                }
            }
        }
        public class DroppedBallInfo
        {
            public BallControl Ball { get; set; }
            public int Column { get; set; }
            public int Row { get; set; }
            public string TokenSkin { get; set; }
        }

        private List<DroppedBallInfo> droppedBalls = new List<DroppedBallInfo>();

        public bool IsAnimationOn { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public PopOutViewModel()
        {
            DatabaseInitializer.Initialize();
            _navigationService = new NavService();
            _gridService = new GridPopService();
            _userService = new UserService();

            NavigateToPickVariantCommand = new RelayCommand<object>(NavigateToPickVariant);
            ToggleModeCommand = new RelayCommand<object>(ToggleMode);
            DropBallCommand = new RelayCommand<int>(DropBall);

            _tokenSkin = token1;
            PlayerTurn = _player1Turn;
            _gridService.DeleteGame(1);
            _gridService.StartNewGame();
        }

        public void SwapTokenSkins()
        => TokenSkin = (TokenSkin == token1 ? token2 : token1);

        public void SwapPlayerTurn()
            => PlayerTurn = (PlayerTurn == _player1Turn ? _player2Turn : _player1Turn);

        public double GetColumnPosition(int index, Grid grid)
        {
            double columnXPosition = 0;
            int i = 0;
            for (; i < index; i++)
            {
                columnXPosition += GetCurrentColumnWidth(i, grid);
            }
            columnXPosition += GetCurrentColumnWidth(i, grid) / 4;
            return columnXPosition;
        }

        public double GetCurrentColumnWidth(int index, Grid grid)
            => grid.ColumnDefinitions[index].ActualWidth;


        public double GetRowPosition(int index, Grid grid)
        {
            double columnYPosition = 0;
            int i = 0;
            for (; i < index; i++)
            {
                columnYPosition += GetCurrentRowHeight(i, grid);
            }
            columnYPosition += GetCurrentRowHeight(i, grid) / 4;
            return columnYPosition;
        }

        public double GetCurrentRowHeight(int index, Grid grid)
            => grid.RowDefinitions[index].ActualHeight;

        public void LoadUser(User user)
        {
            CurrentUser = user;
        }

        private void ToggleMode(object parameter)
        {
            IsPopMode = !IsPopMode;
        }

        public void DropBall(int column)
        {
            if (IsAnimationOn) return; IsAnimationOn = true;
            if (IsPutMode)
            {
                double startXPosition = GetColumnPosition(column, GameGrid);
                double startYPosition = 0; // Set a fixed starting Y position

                BallControl ball = new BallControl();

                Canvas.SetLeft(ball, startXPosition);
                Canvas.SetTop(ball, startYPosition);

                int endRow = _gridService.MakePut(column - 2);

                if(endRow == -1)
                {
                    IsAnimationOn = false;
                    return;
                }

                double endYPosition = GetRowPosition(endRow + 2, GameGrid);

                DoubleAnimation animation = new DoubleAnimation
                {
                    To = endYPosition,
                    Duration = TimeSpan.FromSeconds(1),
                    FillBehavior = FillBehavior.Stop
                };

                ball.Visibility = Visibility.Visible;

                Storyboard.SetTarget(animation, ball);
                Storyboard.SetTargetProperty(animation, new PropertyPath(Canvas.TopProperty));
                Storyboard storyboard = new Storyboard();
                storyboard.Children.Add(animation);

                storyboard.Completed += (sender, e) =>
                {
                    // Adjust token position
                    ball.SetValue(Canvas.TopProperty, endYPosition + 1);
                    ball.SetValue(Canvas.LeftProperty, startXPosition - 3);

                    // Unbind token skin and set it static
                    Image ballImage = ball.FindName("Token") as Image;
                    BindingOperations.ClearBinding(ballImage, Image.SourceProperty);
                    BitmapImage bitmapImage = new BitmapImage(new Uri(TokenSkin, UriKind.RelativeOrAbsolute));
                    ballImage.Source = bitmapImage;

                    // Check end of game
                    if (_gridService.IsWinner() == true)
                    {
                        int winner = _gridService.GetCurrentPlayer();
                        Console.WriteLine("Player " + winner + " has won!");
                    }

                    droppedBalls.Add(new DroppedBallInfo
                    {
                        Ball = ball,
                        Column = column,
                        Row = endRow + 2,
                        TokenSkin = TokenSkin
                    });

                    // Swap players and allow the next move
                    SwapTokenSkins();
                    SwapPlayerTurn();
                    _gridService.SwapPlayers();
                    IsAnimationOn = false;
                };

                GameCanvas.Children.Add(ball);
                storyboard.Begin();
            }
            else
            {

                if (!_gridService.MakePop(column - 2)) {
                    IsAnimationOn = false;
                    return;
                }
                

                int bottomRow = GameGrid.RowDefinitions.Count - 3;

                // Get the bottom ball from the list
                BallControl bottomBall = GetBall(column, bottomRow);

                DoubleAnimation fallOutAnimation = new DoubleAnimation
                {
                    To = GameCanvas.ActualHeight,
                    Duration = TimeSpan.FromSeconds(1),
                    FillBehavior = FillBehavior.Stop
                };

                //bottomBall.Visibility = Visibility.Visible;

                Storyboard.SetTarget(fallOutAnimation, bottomBall);
                Storyboard.SetTargetProperty(fallOutAnimation, new PropertyPath(Canvas.TopProperty));
                Storyboard fallOutStoryboard = new Storyboard();
                fallOutStoryboard.Children.Add(fallOutAnimation);

                fallOutStoryboard.Completed += (sender, e) =>
                {

                    // Check end of game
                    if (_gridService.IsWinner() == true)
                    {
                        int winner = _gridService.GetCurrentPlayer();
                        Console.WriteLine("Player " + winner + " has won!");
                    }
                    // Remove the ball from the canvas
                    GameCanvas.Children.Remove(bottomBall);
                    SwapTokenSkins();
                    SwapPlayerTurn();
                   _gridService.SwapPlayers();
                    IsAnimationOn = false;
                };

                fallOutStoryboard.Begin();

                // Shift the existing balls above down by one row
                for (int row = GameGrid.RowDefinitions.Count - 3; row >= 1; row--)
                {
                    DroppedBallInfo ballAboveInfo = GetBallAndUpdate(column, row - 1);
                    
                    if (ballAboveInfo != null)
                    {
                        BallControl ballAbove = ballAboveInfo.Ball;
                        DoubleAnimation shiftAnimation = new DoubleAnimation
                        {
                            To = GetRowPosition(row, GameGrid),
                            Duration = TimeSpan.FromSeconds(1),
                            FillBehavior = FillBehavior.Stop
                        };

                        Storyboard.SetTarget(shiftAnimation, ballAbove);
                        Storyboard.SetTargetProperty(shiftAnimation, new PropertyPath(Canvas.TopProperty));
                        Storyboard shiftStoryboard = new Storyboard();
                        shiftStoryboard.Children.Add(shiftAnimation);

                        shiftStoryboard.Completed += (s, ev) =>
                        {
                            
                            ballAbove.SetValue(Canvas.TopProperty, GetRowPosition(ballAboveInfo.Row, GameGrid) + 1);
                            ballAbove.SetValue(Canvas.LeftProperty, GetColumnPosition(column, GameGrid) - 3);

                            // Update the grid position of the ball
                            Image ballImage = ballAbove.FindName("Token") as Image;
                            BindingOperations.ClearBinding(ballImage, Image.SourceProperty);
                            BitmapImage bitmapImage = new BitmapImage(new Uri(ballAboveInfo.TokenSkin, UriKind.RelativeOrAbsolute));
                            ballImage.Source = bitmapImage;
                        };

                        shiftStoryboard.Begin();
                    }
                }

                // Update the grid position of the new ball
               Canvas.SetTop(bottomBall, GetRowPosition(bottomRow, GameGrid));
            }
        }

        private DroppedBallInfo GetBallAndUpdate(int column, int row)
        {
            foreach (var ball in droppedBalls)
            {
                if (ball.Column == column && ball.Row == row)
                {
                    ball.Row++;
                    return ball;

                }
            }
            return null;
        }

        private BallControl GetBall(int column, int row)
        {
            foreach (var ball in droppedBalls)
            {
                if (ball.Column == column && ball.Row == row)
                {
                    droppedBalls.Remove(ball);
                    return ball.Ball;
                }
            }
            return null;
        }


        public void NavigateToPickVariant(object obj)
        {
            ExitGamePopUp exitGamePopUp = new ExitGamePopUp();
            bool? result = exitGamePopUp.ShowDialog();
            if (result == true)
            {
                _navigationService.NavigateTo("/PickVariant", CurrentUser);
            }

        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
