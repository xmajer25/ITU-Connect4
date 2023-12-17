using Connect4.DAL.DataModels;
using Connect4.ViewModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NavService = Connect4.Services.NavigationService;
using System.Threading.Tasks;
using Connect4.DAL.DatabaseHelpers;
using Connect4.ViewUserControl;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows;
using System.Diagnostics.Eventing.Reader;
using System.ComponentModel;
using System.Windows.Input;
using Connect4.Commands;
using Connect4.BL.Services;

/*
 * Author   : Jakub Majer (xmajer25)
 * File     : EndScreenViewModel
 * Brief    : Logic for a view shown after game end. Sprinkle animation and if user is logged in data altering.  
 */

namespace Connect4.ViewModel
{
    public class EndScreenViewModel : ILoadUser, ILoadWinner, INotifyPropertyChanged
    {
        public User CurrentUser { get; set; }

        private string _winner = string.Empty;
        public string Winner 
        {
            get { return _winner; }
            set
            {
                if(_winner != value)
                {
                    _winner = value;
                    OnPropertyChanged("Winner");
                }
            }
        }

        private int _reward = 0;
        public int Reward
        {
            get { return _reward; }
            set
            {
                if(_reward != value)
                {
                    _reward = value;
                    OnPropertyChanged("Reward");
                }
            }
        }

        private bool _isUserLoggedIn = false;
        public bool IsUserLoggedIn
        {
            get
            {
                return _isUserLoggedIn;
            }
            set
            {
                if (_isUserLoggedIn != value)
                {
                    _isUserLoggedIn = value;
                    OnPropertyChanged("IsUserLoggedIn");
                }
            }
        }

        private int _currentGold = 0;
        public int CurrentGold
        {
            get { return _currentGold; }
            set
            {
                if (_currentGold != value)
                {
                    _currentGold = value;
                    OnPropertyChanged("CurrentGold");
                }
            }
        }

        private readonly NavService _navigationService;
        private readonly UserService _userService;

        /* VIEW PARTS */
        public Grid MainGrid;
        public Canvas MainCanvas;

        /* COMMANDS */
        public ICommand NavigateToPickVariantCommand { get; private set; }
        public ICommand NavigateToMenuCommand { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public EndScreenViewModel()
        {
            DatabaseInitializer.Initialize();
            _navigationService = new NavService();
            _userService = new UserService();

            NavigateToPickVariantCommand = new RelayCommand<object>(NavigateToPickVariant);
            NavigateToMenuCommand = new RelayCommand<object>(NavigateToMenu);
        }

        /* Sprinkle animation on page loaded */
        public void OnPageLoaded()
        {
            SparkleAnimation();
            if(IsUserLoggedIn)
            {
                CurrentUser = _userService.UpdateUser(
                    CurrentUser.Id, 
                    CurrentUser.Username, 
                    CurrentUser.Password, 
                    CurrentUser.Email, 
                    CurrentUser.GamesPlayed + 1, 
                    CurrentUser.GamesWon + (Reward == 500 ? 1 : 0), 
                    CurrentUser.GoldTotal + 
                    Reward, CurrentUser.GoldActual + Reward
                    );
            }
        }
        
        /* Get right-most position on screen */
        private double GetWindowWidth()
        {
            double maxWindowSize = 0;
            foreach(var column in MainGrid.ColumnDefinitions)
            {
                maxWindowSize += column.ActualWidth;
            }
            return maxWindowSize;
        }

        /* Get lowest position on screen */
        private double GetWindowHeight()
        {
            double maxWindowSize = 0;
            foreach (var row in MainGrid.RowDefinitions)
            {
                maxWindowSize += row.ActualHeight;
            }
            return maxWindowSize;
        }

        /* Animate sparkles on page load. 40 sparkles from bottom left and right with spread towards middle. */
        private void SparkleAnimation()
        {
            Random random = new Random();
            double width = GetWindowWidth();
            double height = GetWindowHeight();

            for (int i = 0; i < 40; i++)
            {
                Particles particleLeft = new Particles();
                Particles particleRight = new Particles();
                particleLeft.Visibility = Visibility.Visible;
                particleRight.Visibility = Visibility.Visible;

                Canvas.SetLeft(particleLeft, 0);
                Canvas.SetTop(particleLeft, height);
                Canvas.SetLeft(particleRight, width);
                Canvas.SetTop(particleRight, height);

                DoubleAnimationUsingKeyFrames animationYLeft = new DoubleAnimationUsingKeyFrames
                {
                    Duration = TimeSpan.FromSeconds(2),
                    FillBehavior = FillBehavior.Stop
                };

                DoubleAnimationUsingKeyFrames animationXLeft = new DoubleAnimationUsingKeyFrames
                {
                    Duration = TimeSpan.FromSeconds(2),
                    FillBehavior = FillBehavior.Stop
                };

                DoubleAnimationUsingKeyFrames animationYRight = new DoubleAnimationUsingKeyFrames
                {
                    Duration = TimeSpan.FromSeconds(2),
                    FillBehavior = FillBehavior.Stop
                };

                DoubleAnimationUsingKeyFrames animationXRight = new DoubleAnimationUsingKeyFrames
                {
                    Duration = TimeSpan.FromSeconds(2),
                    FillBehavior = FillBehavior.Stop
                };

                animationYLeft.KeyFrames.Add(new LinearDoubleKeyFrame(height, KeyTime.FromPercent(0)));
                animationYLeft.KeyFrames.Add(new LinearDoubleKeyFrame(height / 2 + random.Next(-150, 151), KeyTime.FromPercent(1)));

                animationXLeft.KeyFrames.Add(new LinearDoubleKeyFrame(0, KeyTime.FromPercent(0)));
                animationXLeft.KeyFrames.Add(new LinearDoubleKeyFrame(width / 2 + random.Next(-150, 151) - 100, KeyTime.FromPercent(1)));

                animationYRight.KeyFrames.Add(new LinearDoubleKeyFrame(height, KeyTime.FromPercent(0)));
                animationYRight.KeyFrames.Add(new LinearDoubleKeyFrame(height / 2 + random.Next(-150, 151), KeyTime.FromPercent(1)));

                animationXRight.KeyFrames.Add(new LinearDoubleKeyFrame(width, KeyTime.FromPercent(0)));
                animationXRight.KeyFrames.Add(new LinearDoubleKeyFrame(width / 2 + random.Next(-150, 151) + 100, KeyTime.FromPercent(1)));

                Storyboard.SetTarget(animationYLeft, particleLeft);
                Storyboard.SetTargetProperty(animationYLeft, new PropertyPath(Canvas.TopProperty));
                Storyboard.SetTarget(animationXLeft, particleLeft);
                Storyboard.SetTargetProperty(animationXLeft, new PropertyPath(Canvas.LeftProperty));

                Storyboard.SetTarget(animationYRight, particleRight);
                Storyboard.SetTargetProperty(animationYRight, new PropertyPath(Canvas.TopProperty));
                Storyboard.SetTarget(animationXRight, particleRight);
                Storyboard.SetTargetProperty(animationXRight, new PropertyPath(Canvas.LeftProperty));

                Storyboard storyboard = new Storyboard();
                storyboard.Children.Add(animationYLeft);
                storyboard.Children.Add(animationXLeft);
                storyboard.Children.Add(animationYRight);
                storyboard.Children.Add(animationXRight);

                MainCanvas.Children.Add(particleLeft);
                MainCanvas.Children.Add(particleRight);
                storyboard.Begin();
            }
        }


        /* Get logged in user */
        public void LoadUser(User user)
        {
            CurrentUser = user;
            if (user != null)
            {
                CurrentGold = CurrentUser.GoldActual;
                IsUserLoggedIn = true;
            }
        }

        /* Get winner and reward. If user is logged in (user is always player1) set coin reward. */
        public void LoadWinner(int winner)
        {
            if (winner == 0)
            {
                Winner = "It's a draw!";
                Reward = 250;
            }
            else if (winner == 1)
            {
                Winner = "Player 1. has won!";
                Reward = 500;
            }
            else
            {
                Winner = "Player 2. has won!";
                Reward = 0;
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /* Navigation -> go back */
        private void NavigateToPickVariant(object obj)
            => _navigationService.NavigateTo("/PickVariant", CurrentUser);

        /* Navigation -> go to menu */
        private void NavigateToMenu(object obj)
            => _navigationService.NavigateTo("/Menu", CurrentUser);
    }
}
