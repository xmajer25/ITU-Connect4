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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using NavService = Connect4.Services.NavigationService;

namespace Connect4.ViewModel
{
    public class StandardModeViewModel : ILoadUser, INotifyPropertyChanged
    {
        public User CurrentUser { get; set; }

        private readonly NavService _navigationService;
        private GridStandardService _gridService;
        private readonly UserService _userService;
        private readonly UserCustomizableService _userCustomizableService;
        private readonly UserAchievementService _userAchievementService;

        public Grid MainGrid;
        public Grid TopGrid;
        public Grid BottomGrid;
        public Canvas TopCanvas;
        public Canvas BottomCanvas;

        public ICommand NavigateToPickVariantCommand { get; private set; }
        public ICommand DropBallCommand { get; private set; }

        private bool IsAnimationOn { get; set; } = false;

        private readonly string _player1Turn = "Player1 Turn";
        private readonly string _player2Turn = "Player2 Turn";
        private string _playerTurn;

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


        public event PropertyChangedEventHandler PropertyChanged;
        public StandardModeViewModel()
        {
            DatabaseInitializer.Initialize();
            _navigationService = new NavService();
            _gridService = new GridStandardService();
            _userService = new UserService();


            NavigateToPickVariantCommand = new RelayCommand<object>(NavigateToPickVariant);
            DropBallCommand = new RelayCommand<int>(DropBall);

            _tokenSkin = token1;
            PlayerTurn = _player1Turn;
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

        public void PlaceBall(int column, double startingX)
        {
            // Get the row position for the new ball from the service layer
            int maxRow = _gridService.GetMaxRow(column);
            double endYPosition = GetRowPosition(maxRow + 3, BottomGrid);

            BallControl ball = new BallControl();
            Canvas.SetLeft(ball, startingX);
            Canvas.SetTop(ball, 0);

            // Animation for dropping the ball
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
                // Finalize ball's position and appearance
                FinalizeBallPlacement(ball, endYPosition, startingX);

                // Process game state changes and update UI accordingly
                ProcessGameStateChanges();
            };

            BottomCanvas.Children.Add(ball);
            storyboard.Begin();
        }

        private void FinalizeBallPlacement(BallControl ball, double endYPosition, double startingX)
        {
            // Fix token position
            ball.SetValue(Canvas.TopProperty, endYPosition);
            ball.SetValue(Canvas.LeftProperty, startingX - 3);

            // Set token skin statically
            Image ballImage = ball.FindName("Token") as Image;
            BindingOperations.ClearBinding(ballImage, Image.SourceProperty);
            BitmapImage bitmapImage = new BitmapImage(new Uri(TokenSkin, UriKind.RelativeOrAbsolute));
            ballImage.Source = bitmapImage;
        }

        private void ProcessGameStateChanges()
        {
            // Retrieve the updated grid model from the repository
            var result = _gridService.MakeMove();

            // Check for win or draw using the service
            if (_gridService.CheckWinCondition(gridModel))
            {
                // Handle win scenario
                Console.WriteLine("Player has won!");
            }
            else if (_gridService.IsBoardFull(gridModel))
            {
                // Handle draw scenario
                Console.WriteLine("Game ended in a draw");
            }

            // Swap players and skins for the next move
            SwapTokenSkins();
            SwapPlayerTurn();

            // Assuming the service automatically handles player swapping and grid state
            // If not, additional logic may be required here

            // Allow the next move
            IsAnimationOn = false;
        }


        public void LoadUser(User user)
        {
            CurrentUser = user;
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
