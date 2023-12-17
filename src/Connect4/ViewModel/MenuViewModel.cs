using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;
using Connect4.Commands;
using Connect4.BL.Services;
using NavService = Connect4.Services.NavigationService;
using System.Data.Entity;
using Connect4.DAL.DatabaseHelpers;
using Connect4.DAL.Repositories;
using Connect4.DAL.DataModels;
using System.Windows.Controls;
using System.Windows;
using Connect4.ViewModel.Interfaces;
using Connect4.Views.PopUps;

/*
 * Author   : Dušan Slúka (xsluka00)
 * File     : StandardModeViewModel
 * Brief    : ViewModel for standard game mode view. 
 */

namespace Connect4.ViewModel
{
    public class MenuViewModel : INotifyPropertyChanged, ILoadUser
    {
        private User _currentUser = null;
        public User CurrentUser 
        {
            get { return _currentUser; }
            set
            {
                if (_currentUser != value)
                {
                    _currentUser = value;
                    OnPropertyChanged(nameof(CurrentUser));
                    if(value != null)
                    {
                        IsUserLoggedIn = true;
                    }
                }
            }
        }

        /* COMMANDS */
        public ICommand NavigateToSettingsCommand { get; private set; }
        public ICommand NavigateToShopCommand { get; private set; }
        public ICommand ExitAppCommand { get; private set; }
        public ICommand NavigateToLogInCommand { get; private set; }
        public ICommand NavigateToPickVariantCommand { get; private set; }
        public ICommand NavigateToRegisterCommand { get; private set; }
        public ICommand NavigateToProfileCommand { get; private set; }
        private string _loggedInUser;

        private bool _isUserLoggedIn = false;
        public bool IsUserLoggedIn
        {
            get
            {
                return _isUserLoggedIn;
            }
            set
            {
                if( _isUserLoggedIn != value )
                {
                    _isUserLoggedIn = value;
                    OnPropertyChanged("IsUserLoggedIn");
                }
            }
        }

        public string LoggedInUser
        {
            get { return _loggedInUser; }
            set
            {
                _loggedInUser = value;
                OnPropertyChanged(nameof(LoggedInUser));
            }
        }



        /* SERVICES */
        private readonly NavService _navigationService;
        private readonly UserService _userService;

        public MenuViewModel()
        {
            _navigationService = new NavService();
            _userService = new UserService();

            NavigateToSettingsCommand = new RelayCommand<object>(NavigateToSettings);
            NavigateToLogInCommand = new RelayCommand<object>(NavigateToLogIn);
            ExitAppCommand = new RelayCommand<object>(ExitApp);
            NavigateToPickVariantCommand = new RelayCommand<object>(NavigateToPickVariant);
            NavigateToRegisterCommand = new RelayCommand<object>(NavigateToRegister);
            NavigateToProfileCommand = new RelayCommand<object>(NavigateToProfile);
            NavigateToShopCommand = new RelayCommand<object>(NavigateToShop);
        }

        /* Load logged in user */
        public void LoadUser(User user)
        {
            CurrentUser = user;
            if(user != null)
            {
                IsUserLoggedIn = true;
            }
        }

        /* Navigation -> go to user profile */
        public void NavigateToProfile(object obj)
        {
            _navigationService.NavigateTo("/Profile", CurrentUser);
        }

        /* Navigation -> go to register page */
        public void NavigateToRegister(object obj)
        {
            _navigationService.NavigateTo("/Register", CurrentUser);
        }

        /* Application termination */
        public void ExitApp(object obj)
        {
            Application.Current.Shutdown();
        }

        /* Navigation -> go to pick game variant page */
        public void NavigateToPickVariant(object obj)
        {
            _navigationService.NavigateTo("/PickVariant", CurrentUser);
        }

        public void NavigateToShop(object obj)
        {
            _navigationService.NavigateTo("/Shop", CurrentUser);
        }

        /* Navigation -> go to settings */
        private void NavigateToSettings(object obj)
        {

             _navigationService.NavigateTo("/Settings", CurrentUser);

        }

        /* Navigation -> go to log in page*/
        private void NavigateToLogIn(object obj)
        {
            _navigationService.NavigateTo("/LogIn", CurrentUser);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}


