using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Connect4
{
    public partial class App : Application
    {
        private void GlobalMediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            var mediaElement = (MediaElement)sender;
            mediaElement.Position = TimeSpan.Zero;
            mediaElement.Play();
        }
    }
}
