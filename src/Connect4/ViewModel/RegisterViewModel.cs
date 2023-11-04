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
        public string _name { get; set; } = "Name";
        public string _password { get; set; } = "Password";
        public string _passwordRepeat { get; set; } = "Repeat Password";
        public string _email { get; set; } = "Email";
        public string _imgSource { get; set; } = "/Resources/AvatarRn.png";

        public ICommand NavigateBackCommand { get; private set; }
        public ICommand RegisterCommand { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private readonly NavService _navigationService;
        private readonly UserService _userService;

        public RegisterViewModel()
        {
            DatabaseInitializer.Initialize();
            _navigationService = new NavService();
            _userService = new UserService();
            NavigateBackCommand = new RelayCommand<object>(NavigateBack);
            RegisterCommand = new RelayCommand<object>(Register);
        }

        public void NavigateBack(object obj)
        {
            _navigationService.NavigateTo("/LogIn");
        }

        public void Register(object obj)
        {
            if (_userService.IsUserNameRegistered(_name)) return;
            if (_userService.IsUserEmailRegistered(_email)) return;
            if (_password != _passwordRepeat) return;
            _userService.CreateUser(_name, _password, _email);
            NavigateBack(obj);
        }
    }
}
