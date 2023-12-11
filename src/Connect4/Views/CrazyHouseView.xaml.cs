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

namespace Connect4.Views
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

            DataContext = viewModel;
        }
        private void ColumnButton1_Click(object sender, RoutedEventArgs e)
            => StartAnimation(sender, e, 1);
        private void ColumnButton2_Click(object sender, RoutedEventArgs e)
            => StartAnimation(sender, e, 2);
        private void ColumnButton3_Click(object sender, RoutedEventArgs e)
            => StartAnimation(sender, e, 3);
        private void ColumnButton4_Click(object sender, RoutedEventArgs e)
            => StartAnimation(sender, e, 4);
        private void ColumnButton5_Click(object sender, RoutedEventArgs e)
            => StartAnimation(sender, e, 5);
        private void ColumnButton6_Click(object sender, RoutedEventArgs e)
            => StartAnimation(sender, e, 6);
        private void ColumnButton7_Click(object sender, RoutedEventArgs e)
            => StartAnimation(sender, e, 7);
        private void ColumnButton8_Click(object sender, RoutedEventArgs e)
            => StartAnimation(sender, e, 8);

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
    }
}
