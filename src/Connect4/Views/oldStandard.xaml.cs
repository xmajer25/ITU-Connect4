using Connect4.ViewModel;
using System.Windows.Controls;

namespace Connect4.Views
{
    /// <summary>
    /// Interaction logic for StandardModeView.xaml
    /// </summary>
    public partial class oldStandardModeView : Page
    {
        public oldStandardModeView()
        {
            InitializeComponent();
            DataContext = new StandardModeViewModel();
        }
    }
}
