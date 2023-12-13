using Connect4.DAL.DataModels;
using Connect4.Models;
using Connect4.ViewModel;
using Connect4.ViewModel.Interfaces;
using Connect4.Views;
using Connect4.Views.UserViews;
using Connect4.Views.VariantsViews;
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
            new RouteModel("/Settings", typeof(SettingsView), typeof(SettingsViewModel)),
            new RouteModel("/Register", typeof(RegisterView), typeof(RegisterViewModel)),
            new RouteModel("/LogIn", typeof(LogInView), typeof(LogInViewModel)),
            new RouteModel("/PickVariant", typeof(PickVariantView), typeof(PickVariantViewModel)),
            new RouteModel("/StandardMode", typeof(StandardModeView), typeof(StandardModeViewModel)),
            new RouteModel("/CrazyHouseMode", typeof(CrazyHouseView), typeof(CrazyHouseViewModel)),
            new RouteModel("/Profile", typeof(ProfileView), typeof(ProfileViewModel)),
        };

        public void NavigateTo(string uri, User currentUser)
        {
            Type pageType = GetPageFromString(uri);
            Page page = (Page)Activator.CreateInstance(pageType);

            if (page.DataContext is ILoadUser receiveDataPage)
            {
                receiveDataPage.LoadUser(currentUser);
            }

            _window = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            _window.WindowState = WindowState.Maximized;
            _window.Content = page;
        }


        private Type GetPageFromString(string str)
        {
            return Routes.First(route => route.Route == str).ViewType;
        }
    }
}
