using Connect4.BL.Services.GridServices;
using Connect4.Commands;
using Connect4.DAL.DatabaseHelpers;
using Connect4.DAL.DataModels;
using Connect4.ViewModel.Interfaces;
using Connect4.ViewUserControl;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using NavService = Connect4.Services.NavigationService;

namespace Connect4.ViewModel
{
    public class CrazyHouseViewModel : ILoadUser
    {
        public User CurrentUser { get; set; }
        private readonly NavService _navigationService;

        public Grid MainGrid;
        public Grid TopGrid;
        public Canvas TopCanvas;
        public ICommand NavigateToPickVariantCommand { get; private set; }
        public ICommand DropBallCommand { get; private set; }
        public GridCrazyHouseService GridService => new GridCrazyHouseService();

        public CrazyHouseViewModel()
        {
            DatabaseInitializer.Initialize();
            _navigationService = new NavService();

            NavigateToPickVariantCommand = new RelayCommand<object>(NavigateToPickVariant);
            DropBallCommand = new RelayCommand<int>(DropBall);
        }

        public double GetColumnPosition(int index)
        {
            double columnXPosition = 0;

            for (int i = 0; i < index; i++)
            {
                columnXPosition += TopGrid.ColumnDefinitions[i].ActualWidth;
            }
            return columnXPosition;
        }

        public void DropBall(int column)
        {
            BallControl ball = new BallControl();
            
            Canvas.SetLeft(ball, GetColumnPosition(column));  // Set the X position based on the column
            Canvas.SetTop(ball, 0);  // Set the initial Y position

            DoubleAnimation animation = new DoubleAnimation
            {
                To = TopCanvas.ActualHeight,
                Duration = TimeSpan.FromSeconds(3),
                FillBehavior = FillBehavior.Stop
            };

            ball.Visibility = Visibility.Visible;

            Storyboard.SetTarget(animation, ball);
            Storyboard.SetTargetProperty(animation, new PropertyPath(Canvas.TopProperty));
            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(animation);

            storyboard.Completed += (sender, e) =>
            {
                TopCanvas.Children.Remove(ball);
            };

            TopCanvas.Children.Add(ball);
            storyboard.Begin();
        }

        public void LoadUser(User user)
        {
            CurrentUser = user;
        }

        public void NavigateToPickVariant(object obj)
        {
            _navigationService.NavigateTo("/PickVariant", CurrentUser);
        }
    }
}
