using System.Windows.Controls;
using Connect4.ViewModel;
using System.Windows;
using System.Windows.Input;
using System.Diagnostics;
using Connect4.Services;
namespace Connect4.Views.VariantsViews
{
    public partial class StandardModeView : Page
    {
        public StandardModeView()
        {
            
                InitializeComponent();
                StandardModeViewModel viewModel = new StandardModeViewModel();
                viewModel.BottomCanvas = BottomCanvas;
                viewModel.GameGrid = GameGrid;

                DataContext = viewModel;
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
;           var viewModel = DataContext as StandardModeViewModel;
            viewModel?.HandleKeyPress(e.Key);
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // Assuming you have a control named 'InitialControl' that you want to focus
            FirstColumnButton.Focus();
            AudioManager.PlaySound();
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
