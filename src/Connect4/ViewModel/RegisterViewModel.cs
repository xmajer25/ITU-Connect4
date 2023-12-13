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
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using NavService = Connect4.Services.NavigationService;

namespace Connect4.ViewModel
{
    public class RegisterViewModel : ILoadUser, INotifyPropertyChanged
    {
        public User CurrentUser { get; set; }
        public string _name { get; set; } = string.Empty;
        public string _password { get; set; } = string.Empty;
        public string _passwordRepeat { get; set; } = string.Empty;
        public string _email { get; set; } = string.Empty;
        public string _imgSource { get; set; } = "/Resources/Images/Avatars/AvatarRn.png";

        private bool _isNameError = false;
        private string _nameError = string.Empty;
        public bool IsNameError
        {
            get { return _isNameError; }
            set
            {
                if (_isNameError != value)
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
                if(_nameError != value)
                {
                    _nameError = value;
                    OnPropertyChanged("NameError");
                }
            }
        }

        private bool _isEmailError = false;
        private string _emailError = string.Empty;
        public bool IsEmailError
        {
            get { return _isEmailError; }
            set
            {
                if (_isEmailError != value)
                {
                    _isEmailError = value;
                    OnPropertyChanged("IsEmailError");
                }
            }
        }
        public string EmailError
        {
            get { return _emailError; }
            set
            {
                if (_emailError != value)
                {
                    _emailError = value;
                    OnPropertyChanged("EmailError");
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
        private bool _isRepeatPasswordError = false;
        private string _repeatPasswordError = string.Empty;
        public bool IsRepeatPasswordError
        {
            get { return _isRepeatPasswordError; }
            set
            {
                if (_isRepeatPasswordError != value)
                {
                    _isRepeatPasswordError = value;
                    OnPropertyChanged("IsRepeatPasswordError");
                }
            }
        }
        public string RepeatPasswordError
        {
            get { return _repeatPasswordError; }
            set
            {
                if (_repeatPasswordError != value)
                {
                    _repeatPasswordError = value;
                    OnPropertyChanged("RepeatPasswordError");
                }
            }
        }
        public ICommand NavigateToMenuCommand { get; private set; }
        public ICommand NavigateToLogInCommand { get; private set; }
        public ICommand RegisterCommand { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private readonly NavService _navigationService;
        private readonly UserService _userService;

        public RegisterViewModel()
        {
            DatabaseInitializer.Initialize();
            _navigationService = new NavService();
            _userService = new UserService();
            NavigateToMenuCommand = new RelayCommand<object>(NavigateToMenu);
            NavigateToLogInCommand = new RelayCommand<object>(NavigateToLogIn);
            RegisterCommand = new RelayCommand<object>(Register);
        }

        public void LoadUser(User user)
        {
            CurrentUser = user;
        }

        public void NavigateToMenu(object obj)
        {
            _navigationService.NavigateTo("/Menu", CurrentUser);
        }

        public void NavigateToLogIn(object obj)
        {
            _navigationService.NavigateTo("/LogIn", CurrentUser);
        }
        public void Register(object obj)
        {
            bool canRegister = true;

            //Check if name is correct
            if(_name == string.Empty)
            {
                IsNameError = true;
                NameError = "*Fill in your name.";
                canRegister = false;
            }
            else if (_userService.IsUserNameRegistered(_name))
            {
                IsNameError = true;
                NameError = "*Name already exists.";
                canRegister = false;
            }
            else
            {
                IsNameError = false;
                NameError = string.Empty;
            }

            //Check if email is correct
            if(_email == string.Empty)
            {
                IsEmailError = true;
                EmailError = "*Fill in your email";
                canRegister = false;
            }
            else if (_userService.IsUserEmailRegistered(_email))
            {
                IsEmailError = true;
                EmailError = "*Email already exists.";
                canRegister = false;
            }
            else
            {
                IsEmailError = false;
                EmailError = string.Empty;
            }

            //Check if password is correct
            if (_password == string.Empty)
            {
                IsPasswordError = true;
                PasswordError = "*Fill in your password";
                canRegister = false;
            }
            else
            {
                IsPasswordError = false;
                PasswordError = string.Empty;
            }


            if (_passwordRepeat == string.Empty)
            {
                IsRepeatPasswordError = true;
                RepeatPasswordError = "*Repeat password one more time";
                canRegister = false;
            }
            else if (_password != _passwordRepeat)
            {
                IsRepeatPasswordError = true;
                RepeatPasswordError = "*Passwords do not match.";
                canRegister = false;
            }
            else
            {
                IsRepeatPasswordError = false;
                RepeatPasswordError = string.Empty;
            }
            


            if (!canRegister) return;

            _userService.CreateUser(_name, _password, _email);
            NavigateToLogIn(obj);
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
