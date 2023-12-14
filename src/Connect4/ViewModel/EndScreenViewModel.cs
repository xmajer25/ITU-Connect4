﻿using Connect4.DAL.DataModels;
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

        private readonly NavService _navigationService;

        public Grid MainGrid;
        public Canvas MainCanvas;

        public ICommand NavigateToPickVariantCommand { get; private set; }
        public ICommand NavigateToMenuCommand { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public EndScreenViewModel()
        {
            DatabaseInitializer.Initialize();
            _navigationService = new NavService();

            NavigateToPickVariantCommand = new RelayCommand<object>(NavigateToPickVariant);
            NavigateToMenuCommand = new RelayCommand<object>(NavigateToMenu);
        }
        public void OnPageLoaded()
            => SparkleAnimation();
        

        private double GetWindowWidth()
        {
            double maxWindowSize = 0;
            foreach(var column in MainGrid.ColumnDefinitions)
            {
                maxWindowSize += column.ActualWidth;
            }
            return maxWindowSize;
        }

        private double GetWindowHeight()
        {
            double maxWindowSize = 0;
            foreach (var row in MainGrid.RowDefinitions)
            {
                maxWindowSize += row.ActualHeight;
            }
            return maxWindowSize;
        }

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



        public void LoadUser(User user)
        {
            CurrentUser = user;
            if (user != null)
            {
                IsUserLoggedIn = true;
            }
        }

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

        private void NavigateToPickVariant(object obj)
            => _navigationService.NavigateTo("/PickVariant", CurrentUser);

        private void NavigateToMenu(object obj)
            => _navigationService.NavigateTo("/Menu", CurrentUser);
    }
}