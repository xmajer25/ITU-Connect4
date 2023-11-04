using Connect4.ViewModel;
using System.Windows.Controls;

namespace Connect4.Views
{
    /// <summary>
    /// Interaction logic for StandardModeView.xaml
    /// </summary>
    public partial class StandardModeView : Page
    {
        public StandardModeView()
        {
            InitializeComponent();
            DataContext = new StandardModeViewModel();
        }
    }
}
