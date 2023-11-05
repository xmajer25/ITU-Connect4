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
using System.Windows.Input;
using NavService = Connect4.Services.NavigationService;

namespace Connect4.ViewModel
{
    public class LogInViewModel : INotifyPropertyChanged
    {
        public ICommand NavigateToMenuCommand { get; private set; }
        public ICommand NavigateToRegisterCommand { get; private set; }
        public ICommand LogInCommand { get; private set; }
        public string _name { get; set; } = string.Empty;
        public string _password { get; set; } = string.Empty;


        public event PropertyChangedEventHandler PropertyChanged;
        private readonly NavService _navigationService;
        private readonly UserService _userService;

        public LogInViewModel()
        {
            DatabaseInitializer.Initialize();
            _navigationService = new NavService();
            _userService = new UserService();
            NavigateToMenuCommand = new RelayCommand<object>(NavigateToMenu); 
            NavigateToRegisterCommand = new RelayCommand<object>(NavigateToRegister);
            LogInCommand = new RelayCommand<object>(LogIn);
        }

        public void NavigateToMenu(object obj)
        {
            _navigationService.NavigateTo("/Menu");
        }

        public void NavigateToRegister(object obj)
        {
            _navigationService.NavigateTo("/Register");
        }

        public void LogIn(object obj)
        {
            //TODO
        }
    }
}
