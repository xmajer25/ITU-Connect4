using Connect4.ViewModel;
using System.Windows;
using System.Windows.Controls;


namespace Connect4.Views
{
   public partial class EndScreenView : Page
    {
        private EndScreenViewModel viewModel;
        public EndScreenView()
        {
            InitializeComponent();
            viewModel = new EndScreenViewModel();
            viewModel.MainGrid = MainGrid;
            viewModel.MainCanvas = MainCanvas;
            DataContext = viewModel;
        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            viewModel.OnPageLoaded();
        }
    }
}
