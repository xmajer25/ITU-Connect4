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
using System.Windows.Shapes;

namespace Connect4.Views.PopUps
{
    /// <summary>
    /// Interaction logic for ExitGamePopUp.xaml
    /// </summary>
    public partial class ExitGamePopUp : Window
    {
        public ExitGamePopUp()
        {
            InitializeComponent();
        }

        private void Option1_Click(object sender, RoutedEventArgs e)
        {
            // Set DialogResult to true or false based on the user's choice
            DialogResult = true;
            Close();
        }

        private void Option2_Click(object sender, RoutedEventArgs e)
        {
            // Set DialogResult to true or false based on the user's choice
            DialogResult = false;
            Close();
        }
    }
}
