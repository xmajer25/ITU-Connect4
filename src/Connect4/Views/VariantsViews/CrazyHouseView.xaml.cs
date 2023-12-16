using Connect4.Services;
using Connect4.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Connect4.Views.VariantsViews
{
    /// <summary>
    /// Interaction logic for CrazyHouseView.xaml
    /// </summary>
    public partial class CrazyHouseView : Page
    {
        public CrazyHouseView()
        {
            InitializeComponent();
            CrazyHouseViewModel viewModel = new CrazyHouseViewModel();
            viewModel.MainGrid = MainGrid;
            viewModel.TopGrid = TopGrid;
            viewModel.TopCanvas = TopCanvas;
            viewModel.BottomCanvas = BottomCanvas;
            viewModel.BottomGrid = BottomGrid;

            DataContext = viewModel;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            AudioManager.PlaySound();
        }

        private void ColumnButton4_Click(object sender, RoutedEventArgs e)
            => StartAnimation(sender, e, 4);
        private void ColumnButton5_Click(object sender, RoutedEventArgs e)
            => StartAnimation(sender, e, 5);

        private void StartAnimation(object sender, RoutedEventArgs e, int column)
        {
            if (sender is Button)
            {
                if (DataContext is CrazyHouseViewModel viewModel)
                {
                    viewModel.DropBallCommand.Execute(column); 
                }
            }
        }

        private void ShowLeftDrop(object sender, MouseEventArgs e)
        {
            LeftDrop.Visibility = Visibility.Visible;
        }

        private void HideLeftDrop(object sender, MouseEventArgs e)
        {
            LeftDrop.Visibility = Visibility.Collapsed;
        }

        private void ShowRightDrop(object sender, MouseEventArgs e)
        {
            RightDrop.Visibility = Visibility.Visible;
        }

        private void HideRightDrop(object sender, MouseEventArgs e)
        {
            RightDrop.Visibility = Visibility.Collapsed;
        }
    }
}
