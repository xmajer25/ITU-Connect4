using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Connect4.Services
{
    public class NavigationService
    {
        public void NavigateTo(string uri)
        {
            var mainWin = Application.Current.MainWindow;
            mainWin.Content = new Frame { Source = new Uri(uri, UriKind.RelativeOrAbsolute) };
        }
    }

}
