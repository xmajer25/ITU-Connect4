using Connect4.Models;
using Connect4.ViewModel;
using Connect4.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Navigation;
using Menu = Connect4.Views.Menu;

namespace Connect4.Services
{
    public class NavigationService
    {
        Window _window;
        public NavigationService()
        {
            _window = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
        }
        public IEnumerable<RouteModel> Routes { get; } = new List<RouteModel>
    {
        new RouteModel("/Menu", typeof(Menu), typeof(MenuViewModel)),
        new RouteModel("/Setting", typeof(Settings), typeof(SettingsViewModel)),
        new RouteModel("/Register", typeof(RegisterView), typeof(RegisterViewModel)),
        new RouteModel("/LogIn", typeof(LogInView), typeof(LogInViewModel)),
        new RouteModel("/PickVariant", typeof(PickVariantView), typeof(PickVariantViewModel)),
        new RouteModel("/StandardMode", typeof(StandardModeView), typeof(StandardModeViewModel)),
        new RouteModel("/CrazyHouseMode", typeof(CrazyHouseView), typeof(CrazyHouseViewModel)),
    };

        public void NavigateTo(string uri)
        {
            Type pageType = GetPageFromString(uri);
            Page page = (Page)Activator.CreateInstance(pageType);

            /*if (page is IReceiveData receiveDataPage)
            {
                // If the page implements a specific interface for receiving data
                receiveDataPage.ReceiveData(data);
            }*/

            _window = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            _window.Content = page;
        }


        private Type GetPageFromString(string str)
        {
            return Routes.First(route => route.Route == str).ViewType;
        }
    }
}
