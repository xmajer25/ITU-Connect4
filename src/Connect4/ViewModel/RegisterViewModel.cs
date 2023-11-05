using Connect4.BL.Services;
using Connect4.Commands;
using Connect4.DAL.DatabaseHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using NavService = Connect4.Services.NavigationService;

namespace Connect4.ViewModel
{
    public class RegisterViewModel
    {
        public string _name { get; set; } = string.Empty;
        public string _password { get; set; } = string.Empty;
        public string _passwordRepeat { get; set; } = string.Empty;
        public string _email { get; set; } = string.Empty;
        public string _imgSource { get; set; } = "/Resources/Images/AvatarRn.png";

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

        public void NavigateToMenu(object obj)
        {
            _navigationService.NavigateTo("/Menu");
        }

        public void NavigateToLogIn(object obj)
        {
            _navigationService.NavigateTo("/LogIn");
        }
        public void Register(object obj)
        {
            //Check empty string
            if (_name == string.Empty ||
                _email == string.Empty ||
                _password == string.Empty ||
                _passwordRepeat == string.Empty) return;

            //Check if not exist
            if (_userService.IsUserNameRegistered(_name)) return;
            if (_userService.IsUserEmailRegistered(_email)) return;

            if (_password != _passwordRepeat) return;

            _userService.CreateUser(_name, _password, _email);
            NavigateToLogIn(obj);
        }
    }
}
