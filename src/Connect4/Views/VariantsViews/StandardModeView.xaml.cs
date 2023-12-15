using System.Windows.Controls;
using Connect4.ViewModel;
using System.Windows;
using System.Windows.Input;
namespace Connect4.Views.VariantsViews
{
    public partial class StandardModeView : Page
    {
        public StandardModeView()
        {
            
                InitializeComponent();
                StandardModeViewModel viewModel = new StandardModeViewModel();
                viewModel.MainGrid = MainGrid;
                //viewModel.TopCanvas = TopCanvas;
                viewModel.BottomCanvas = BottomCanvas;
                viewModel.GameGrid = GameGrid;

                DataContext = viewModel;
        }
        private void ColumnButton1_Click(object sender, RoutedEventArgs e)
            => StartAnimation(sender, e, 2);
        private void ColumnButton2_Click(object sender, RoutedEventArgs e)
            => StartAnimation(sender, e, 3);
        private void ColumnButton3_Click(object sender, RoutedEventArgs e)
            => StartAnimation(sender, e, 4);
        private void ColumnButton4_Click(object sender, RoutedEventArgs e)
            => StartAnimation(sender, e, 5);
        private void ColumnButton5_Click(object sender, RoutedEventArgs e)
            => StartAnimation(sender, e, 6);
        private void ColumnButton6_Click(object sender, RoutedEventArgs e)
            => StartAnimation(sender, e, 7);
        private void ColumnButton7_Click(object sender, RoutedEventArgs e)
            => StartAnimation(sender, e, 8);
        private void ColumnButton8_Click(object sender, RoutedEventArgs e)
            => StartAnimation(sender, e, 9);

        private void StartAnimation(object sender, RoutedEventArgs e, int column)
        {
            if (sender is Button)
            {
                if (DataContext is StandardModeViewModel viewModel)
                {
                    viewModel.PlaceBallCommand.Execute(column);
                }
            }
        }

        private void Show1Drop(object sender, MouseEventArgs e)
        {
            Drop1.Visibility = Visibility.Visible;
        }
        private void Show2Drop(object sender, MouseEventArgs e)
        {
            Drop2.Visibility = Visibility.Visible;
        }
        private void Show3Drop(object sender, MouseEventArgs e)
        {
            Drop3.Visibility = Visibility.Visible;
        }
        private void Show4Drop(object sender, MouseEventArgs e)
        {
            Drop4.Visibility = Visibility.Visible;
        }
        private void Show5Drop(object sender, MouseEventArgs e)
        {
            Drop5.Visibility = Visibility.Visible;
        }
        private void Show6Drop(object sender, MouseEventArgs e)
        {
            Drop6.Visibility = Visibility.Visible;
        }
        private void Show7Drop(object sender, MouseEventArgs e)
        {
            Drop7.Visibility = Visibility.Visible;
        }
        private void Show8Drop(object sender, MouseEventArgs e)
        {
            Drop8.Visibility = Visibility.Visible;
        }

        private void Hide1tDrop(object sender, MouseEventArgs e)
        {
            Drop1.Visibility = Visibility.Collapsed;
        }

        private void Hide2tDrop(object sender, MouseEventArgs e)
        {
            Drop2.Visibility = Visibility.Collapsed;
        }
        private void Hide3tDrop(object sender, MouseEventArgs e)
        {
            Drop3.Visibility = Visibility.Collapsed;
        }
        private void Hide4tDrop(object sender, MouseEventArgs e)
        {
            Drop4.Visibility = Visibility.Collapsed;
        }
        private void Hide5tDrop(object sender, MouseEventArgs e)
        {
            Drop5.Visibility = Visibility.Collapsed;
        }
        private void Hide6tDrop(object sender, MouseEventArgs e)
        {
            Drop6.Visibility = Visibility.Collapsed;
        }
        private void Hide7tDrop(object sender, MouseEventArgs e)
        {
            Drop7.Visibility = Visibility.Collapsed;
        }
        private void Hide8tDrop(object sender, MouseEventArgs e)
        {
            Drop8.Visibility = Visibility.Collapsed;
        }
    }
}
