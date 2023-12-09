using Connect4.BL.Services;
using Connect4.Commands;
using Connect4.DAL.DatabaseHelpers;
using Connect4.DAL.DataModels;
using Connect4.ViewModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using NavService = Connect4.Services.NavigationService;

namespace Connect4.ViewModel
{
    public class StandardModeViewModel : ILoadUser
    {
        public User CurrentUser { get; set; }
        private readonly NavService _navigationService;
        public ICommand MouseMoveCommand { get; private set; }

        public StandardModeViewModel()
        {
            DatabaseInitializer.Initialize();
            // Initialize commands
            MouseMoveCommand = new RelayCommand<MouseEventArgs>(OnMouseMove);
            // Remove any navigation-related properties and methods if they're not needed
        }

        private Visibility _arrowVisibility = Visibility.Collapsed;
        private Thickness _arrowMargin = new Thickness(0);

        public Visibility ArrowVisibility
        {
            get => _arrowVisibility;
            set
            {
                _arrowVisibility = value;
                OnPropertyChanged(nameof(ArrowVisibility));
            }
        }

        public Thickness ArrowMargin
        {
            get => _arrowMargin;
            set
            {
                _arrowMargin = value;
                OnPropertyChanged(nameof(ArrowMargin));
            }
        }

        public void LoadUser(User user)
        {
            CurrentUser = user;
        }
        
        private void OnMouseMove(MouseEventArgs e)
        {
            if (e.OriginalSource is UIElement element)
            {
                var point = e.GetPosition(element);
                CheckMousePosition(point);
            }
        }

        private void CheckMousePosition(Point point)
        {
            // Define the start X position of each column based on your column widths and margins
            double[] columnStartX = { 190, 242, 294, 346, 398, 450, 502 }; // Adjusted for 7 columns
            double columnWidth = 52; // Use the max column width if they vary slightly

            bool columnFound = false;

            // Determine if the point's X position is within any of the columns
            for (int i = 0; i < columnStartX.Length; i++)
            {
                if (point.X >= columnStartX[i] && point.X < (columnStartX[i] + columnWidth))
                {
                    // Mouse is within column i
                    ArrowMargin = new Thickness(columnStartX[i], 1, 1, 1);
                    Console.WriteLine($"Column: {i + 1}");
                    ArrowVisibility = Visibility.Visible;
                    columnFound = true;
                    break;
                }
            }

            if (!columnFound)
            {
                ArrowVisibility = Visibility.Collapsed;
            }
        }

        // Implement INotifyPropertyChanged interface
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
