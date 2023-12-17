using Connect4.BL.Services;
using Connect4.Commands;
using Connect4.DAL.DatabaseHelpers;
using Connect4.DAL.DataModels;
using Connect4.ViewModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using NavService = Connect4.Services.NavigationService;

/*
 * Author   : Jakub Majer (xmajer25)
 * File     : LogInViewModel
 * Brief    : Log In Page Logic 
 */

namespace Connect4.ViewModel
{
    public class LogInViewModel : INotifyPropertyChanged, ILoadUser
    {
        public User CurrentUser { get; set; }

        /* COMMANDS */
        public ICommand NavigateToMenuCommand { get; private set; }
        public ICommand NavigateToRegisterCommand { get; private set; }
        public ICommand LogInCommand { get; private set; }


        public string _name { get; set; } = string.Empty;
        private bool _isNameError = false;
        private string _nameError = string.Empty;
        public bool IsNameError
        {
            get { return _isNameError; }
            set
            {
                if( _isNameError != value )
                {
                    _isNameError = value;
                    OnPropertyChanged("IsNameError");
                }
            }
        }
        public string NameError
        {
            get { return _nameError; }
            set
            {
                if( _nameError != value )
                {
                    _nameError = value;
                    OnPropertyChanged("NameError");
                }
            }
        }

        private bool _isPasswordError = false;
        private string _passwordError = string.Empty;
        public bool IsPasswordError
        {
            get { return _isPasswordError; }
            set
            {
                if (_isPasswordError != value)
                {
                    _isPasswordError = value;
                    OnPropertyChanged("IsPasswordError");
                }
            }
        }
        public string PasswordError
        {
            get { return _passwordError; }
            set
            {
                if (_passwordError != value)
                {
                    _passwordError = value;
                    OnPropertyChanged("PasswordError");
                }
            }
        }

        public string _password { get; set; } = string.Empty;


        public event PropertyChangedEventHandler PropertyChanged;

        /* SERVICES */
        private readonly NavService _navigationService;
        private readonly UserService _userService;

        public LogInViewModel()
        {
            //DatabaseInitializer.Initialize();
            _navigationService = new NavService();
            _userService = new UserService();
            NavigateToMenuCommand = new RelayCommand<object>(NavigateToMenu); 
            NavigateToRegisterCommand = new RelayCommand<object>(NavigateToRegister);
            LogInCommand = new RelayCommand<object>(LogIn);
        }

        public void LoadUser(User user)
        {
            CurrentUser = user;
        }

        /* Navigation -> go to menu*/
        public void NavigateToMenu(object obj)
        {
            _navigationService.NavigateTo("/Menu", CurrentUser);
        }

        /* Navigation -> go to register page*/
        public void NavigateToRegister(object obj)
        {
            _navigationService.NavigateTo("/Register", CurrentUser);
        }


        /* Log in logic */
        public void LogIn(object obj)
        {
            bool canLogIn = true;
            if(_password == string.Empty) 
            {
                IsPasswordError = true;
                PasswordError = "*Please fill in your password.";
                canLogIn = false;
            }
            else
            {
                IsPasswordError = false;
                PasswordError = string.Empty;
            }

            if (_name == string.Empty)
            {
                IsNameError = true;
                NameError = "*Please fill in your name.";
                canLogIn= false;
            }
            else
            {
                IsNameError = false;
                NameError = string.Empty;
            }

            if (!canLogIn) return;

            User logInUser = _userService.GetUserByUsername(_name);
            if(logInUser == null)
            {
                IsNameError = true;
                NameError = "*Name or Password are incorect.";
                return;
            }
            if(_password != logInUser.Password) 
            {
                IsNameError = true;
                NameError = "*Name or Password are incorect.";
                return;
            }

            CurrentUser = logInUser;
            _navigationService.NavigateTo("/Menu", CurrentUser);
        }

         protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
