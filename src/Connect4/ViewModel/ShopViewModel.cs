using Connect4.Commands;
using Connect4.DAL.DatabaseHelpers;
using Connect4.DAL.DataModels;
using Connect4.ViewModel.Interfaces;
using Connect4.BL.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using NavService = Connect4.Services.NavigationService;

namespace Connect4.ViewModel
{
    public class ShopViewModel : INotifyPropertyChanged, ILoadUser
    {
        public User CurrentUser { get; set; }

        private string _username = string.Empty;

        /* COMMANDS */
        public ICommand NavigateToMenuCommand { get; private set; }

        /* SERVICES */
        private readonly NavService _navigationService;
        private readonly CustomizableService _customService;
        private readonly UserCustomizableService _userCustomizableService;

        public event PropertyChangedEventHandler PropertyChanged;

        public ShopViewModel()
        {
            DatabaseInitializer.Initialize();
            _navigationService = new NavService();
            NavigateToMenuCommand = new RelayCommand<object>(NavigateToMenu);
        }

        /* Load user */
        public void LoadUser(User user)
        {
            CurrentUser = user;

            if (CurrentUser != null)
            {
                _username = CurrentUser.Username;
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void NavigateToMenu(object obj)
        {
            _navigationService.NavigateTo("/Menu", CurrentUser);
        }
    }
}
