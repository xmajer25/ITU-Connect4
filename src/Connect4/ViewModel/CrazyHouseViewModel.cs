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
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using NavService = Connect4.Services.NavigationService;

/*
 * Author   : Jakub Majer (xmajer25)
 * File     : CrazyHouseViewModel
 * Brief    : Logic behind a game variant with randomized ball drops. Mostly animations and game logic.   
 */

namespace Connect4.ViewModel
{
    public class CrazyHouseViewModel : ILoadUser, INotifyPropertyChanged
    {
        public User CurrentUser { get; set; }

        /* SERVICES */
        private readonly NavService _navigationService;
        private GridCrazyHouseService _gridService;
        private readonly UserService _userService;
        private readonly CustomizableService _customizableService;

        /* VIEW PARTS */
        public Grid MainGrid;
        public Grid TopGrid;
        public Grid BottomGrid;
        public Canvas TopCanvas;
        public Canvas BottomCanvas;

        /* COMMANDS */
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
                if(_tokenSkin != value)
                {
                    _tokenSkin = value;
                    OnPropertyChanged("TokenSkin");
                }
            }
        }

        private string _background = "/Resources/Images/BackGrounds/BackGroundDefault.png";
        public string BackGround
        {
            get { return _background; }
            set
            {
                if (_background != value)
                {
                    _background = value;
                    OnPropertyChanged("BackGround");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public CrazyHouseViewModel()
        {
            _navigationService = new NavService();
            _gridService = new GridCrazyHouseService();
            _userService = new UserService();
            _customizableService = new CustomizableService();

            NavigateToPickVariantCommand = new RelayCommand<object>(NavigateToPickVariant);
            DropBallCommand = new RelayCommand<int>(DropBall);


            PlayerTurn = _player1Turn;
            BackGround = _background;
        }

        /* Loads Currently Logged In User */
        public void LoadUser(User user)
        {
            CurrentUser = user;
            if(CurrentUser != null)
            {
                token1 = _customizableService.GetCustomizablesForUser(CurrentUser.Id, 1, 1)[0];
                token2 = _customizableService.GetCustomizablesForUser(CurrentUser.Id, 2, 1)[0];
                TokenSkin = token1;

                _background = _customizableService.GetCustomizablesForUser(CurrentUser.Id, 1, 0)[0];
                BackGround = _background;
            }
        }

        public void SwapTokenSkins()
         => TokenSkin = (TokenSkin == token1 ? token2 : token1);

        public void SwapPlayerTurn()
            => PlayerTurn = (PlayerTurn == _player1Turn ? _player2Turn : _player1Turn);


        /* Gets x position of a column in view */
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

        /* Gets Width of a column in view */
        public double GetCurrentColumnWidth(int index, Grid grid)
            => grid.ColumnDefinitions[index].ActualWidth;
        
        /* Gets y position of a row in view */
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

        /* Gets Height of a row in view */
        public double GetCurrentRowHeight(int index, Grid grid)
            => grid.RowDefinitions[index].ActualHeight;

        /* Animation for randomized ball dorp*/
        public void DropBall(int column)
        {
            if (IsAnimationOn) return; IsAnimationOn = true;
            BallControl ball = new BallControl();
            
            double bounceXPosition = GetCurrentColumnWidth(column, TopGrid);
            double firstRowDrop = GetCurrentRowHeight(0, TopGrid);
            double bounceYPosition = GetCurrentRowHeight(1, TopGrid);
            double startXPosition = GetColumnPosition(column, TopGrid);

            List<bool> path;
            int endingColumn;

            (path, endingColumn) = _gridService.PlayMove(column);

            Canvas.SetLeft(ball, startXPosition);
            Canvas.SetTop(ball, 0);

            

            DoubleAnimationUsingKeyFrames animationY = new DoubleAnimationUsingKeyFrames
            {
                Duration = TimeSpan.FromSeconds(2),
                FillBehavior = FillBehavior.Stop
            };

            DoubleAnimationUsingKeyFrames animationX = new DoubleAnimationUsingKeyFrames
            {
                Duration = TimeSpan.FromSeconds(2),
                FillBehavior = FillBehavior.Stop
            };

            // Define keyframes for the Y position
            animationY.KeyFrames.Add(new LinearDoubleKeyFrame(0, KeyTime.FromPercent(0)));
            animationY.KeyFrames.Add(new LinearDoubleKeyFrame(firstRowDrop, KeyTime.FromPercent(0.11)));
            animationY.KeyFrames.Add(new LinearDoubleKeyFrame(-bounceYPosition / 4 + firstRowDrop, KeyTime.FromPercent(0.14)));
            animationY.KeyFrames.Add(new LinearDoubleKeyFrame(bounceYPosition + firstRowDrop, KeyTime.FromPercent(0.22)));
            animationY.KeyFrames.Add(new LinearDoubleKeyFrame(-bounceYPosition / 4 + firstRowDrop + bounceYPosition, KeyTime.FromPercent(0.25)));
            animationY.KeyFrames.Add(new LinearDoubleKeyFrame(bounceYPosition * 2 + firstRowDrop, KeyTime.FromPercent(0.33)));
            animationY.KeyFrames.Add(new LinearDoubleKeyFrame(-bounceYPosition / 4 + firstRowDrop + bounceYPosition * 2, KeyTime.FromPercent(0.36)));
            animationY.KeyFrames.Add(new LinearDoubleKeyFrame(bounceYPosition * 3 + firstRowDrop, KeyTime.FromPercent(0.44)));
            animationY.KeyFrames.Add(new LinearDoubleKeyFrame(-bounceYPosition / 4 + firstRowDrop + bounceYPosition * 3, KeyTime.FromPercent(0.47)));
            animationY.KeyFrames.Add(new LinearDoubleKeyFrame(bounceYPosition * 4 + firstRowDrop, KeyTime.FromPercent(0.55)));
            animationY.KeyFrames.Add(new LinearDoubleKeyFrame(-bounceYPosition / 4 + firstRowDrop + bounceYPosition * 4, KeyTime.FromPercent(0.58)));
            animationY.KeyFrames.Add(new LinearDoubleKeyFrame(bounceYPosition * 5 + firstRowDrop, KeyTime.FromPercent(0.66)));
            animationY.KeyFrames.Add(new LinearDoubleKeyFrame(-bounceYPosition / 4 + firstRowDrop + bounceYPosition * 5, KeyTime.FromPercent(0.69)));
            animationY.KeyFrames.Add(new LinearDoubleKeyFrame(bounceYPosition * 6 + firstRowDrop, KeyTime.FromPercent(0.77)));
            animationY.KeyFrames.Add(new LinearDoubleKeyFrame(-bounceYPosition / 4 + firstRowDrop + bounceYPosition * 6, KeyTime.FromPercent(0.80)));
            animationY.KeyFrames.Add(new LinearDoubleKeyFrame(TopCanvas.ActualHeight, KeyTime.FromPercent(1))); 

            // Define keyframes for the X position
            animationX.KeyFrames.Add(new LinearDoubleKeyFrame(Canvas.GetLeft(ball), KeyTime.FromPercent(0)));
            animationX.KeyFrames.Add(new LinearDoubleKeyFrame(Canvas.GetLeft(ball) - bounceXPosition, KeyTime.FromPercent(0.11)));

            double currentTime = 0.11;
            double currentPosition = Canvas.GetLeft(ball) - bounceXPosition;
            double endPosition = currentPosition;

            for (int i = 0; i < path.Count; i++)
            {
                var direction = path[i];
                currentTime += 0.11;
                double nextMove = direction ? bounceXPosition : (-bounceXPosition);
                if (i == 5 && path.Count == 6)
                {   
                    endPosition += nextMove / 2;
                    animationX.KeyFrames.Add(new LinearDoubleKeyFrame(endPosition, KeyTime.FromPercent(currentTime)));
                }
                else if(i == 6)
                {
                    endPosition += nextMove /2;
                    animationX.KeyFrames.Add(new LinearDoubleKeyFrame(endPosition, KeyTime.FromPercent(currentTime)));
                }
                else
                {
                    currentPosition += nextMove / 2;
                    endPosition += nextMove;
                    animationX.KeyFrames.Add(new LinearDoubleKeyFrame(currentPosition - (nextMove / 2), KeyTime.FromPercent(currentTime)));
                }
            }
           

            ball.Visibility = Visibility.Visible;

            Storyboard.SetTarget(animationY, ball);
            Storyboard.SetTargetProperty(animationY, new PropertyPath(Canvas.TopProperty));

            Storyboard.SetTarget(animationX, ball);
            Storyboard.SetTargetProperty(animationX, new PropertyPath(Canvas.LeftProperty));


            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(animationY);
            storyboard.Children.Add(animationX);

            /* ON ANIMATION COMPLETED */
            storyboard.Completed += (sender, e) =>
            {
                TopCanvas.Children.Remove(ball);
                PlaceBall(endingColumn, endPosition);
            };

            /* START ANIMATION */
            TopCanvas.Children.Add(ball);
            storyboard.Begin();
        }

        /* Animation to place ball inside Grid */
        public void PlaceBall(int column, double startingX)
        {
            double endYPosition = GetRowPosition(_gridService.GetMaxRow(column) + 3, BottomGrid);
            BallControl ball = new BallControl();

            Canvas.SetLeft(ball, startingX);  // Set the X position based on the column
            Canvas.SetTop(ball, 0);
            
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

            /* ON ANIMATION COMPLETED */
            storyboard.Completed += (sender, e) =>
            {
                //FIX TOKEN POSITION
                ball.SetValue(Canvas.TopProperty, endYPosition);
                ball.SetValue(Canvas.LeftProperty, startingX - 3);

                //UNBIND TOKEN SKIN AND SET IT STATIC
                Image ballImage = ball.FindName("Token") as Image;
                BindingOperations.ClearBinding(ballImage, Image.SourceProperty);
                BitmapImage bitmapImage = new BitmapImage(new Uri(TokenSkin, UriKind.RelativeOrAbsolute));
                ballImage.Source = bitmapImage;

                //CHECK END OF GAME
                if(_gridService.CheckForWin() == true)
                {
                    int winner = _gridService.GetPlayer();
                    _navigationService.NavigateTo("/EndScreen", CurrentUser, winner);
                }
                else if(_gridService.IsGridFull() == true)
                {
                    _navigationService.NavigateTo("/EndScreen", CurrentUser, 0);
                }

                //SWAP PLAYERS AND ALLOW NEXT MOVE
                SwapTokenSkins();
                SwapPlayerTurn();
                _gridService.SwapPlayers();
                IsAnimationOn = false;
            };

            /* START ANIMATION */
            BottomCanvas.Children.Add(ball);
            storyboard.Begin();
        }


        /* Navigation Command -> go back*/
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
