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

namespace Connect4.Views
{
   /*
    * Author   : Ivan Mahút (xmahut01)
    * File     : ShopView
    * Brief    : Implements logic of interaction within View
  */
    public partial class ShopView : Page
    {
        public ShopView()
        {
            InitializeComponent();
            DataContext = new ShopViewModel();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            AudioManager.PlaySound();
        }
    }
}
