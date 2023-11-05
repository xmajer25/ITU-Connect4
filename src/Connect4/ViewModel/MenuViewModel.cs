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
using Connect4.BL.Models;
using NavService = Connect4.Services.NavigationService;
using System.Data.Entity;
using Connect4.DAL.DatabaseHelpers;
using Connect4.DAL.Repositories;
using Connect4.DAL.DataModels;
using System.Windows.Controls;
using System.Windows;

namespace Connect4.ViewModel
{
    public class MenuViewModel : INotifyPropertyChanged
    {
        public ICommand NavigateToSettingsCommand { get; private set; }
        public ICommand ExitAppCommand { get; private set; }
        public ICommand NavigateToLogInCommand { get; private set; }
        public ICommand NavigateToPickVariantCommand { get; private set; }
        public ICommand NavigateToRegisterCommand { get; private set; }
        public ICommand LogInCommand { get; private set; }
        private string _loggedInUser;

        public string LoggedInUser
        {
            get { return _loggedInUser; }
            set
            {
                _loggedInUser = value;
                OnPropertyChanged(nameof(LoggedInUser));
            }
        }




        private readonly NavService _navigationService;
        private readonly UserService _userService;
        private ObservableCollection<string> _usernames;

        public MenuViewModel()
        {
            DatabaseInitializer.Initialize();
            _navigationService = new NavService();
            _userService = new UserService();
            Usernames = new ObservableCollection<string>(_userService.GetAllUsernames());

            LogInCommand = new RelayCommand<object>(LogIn);
            NavigateToSettingsCommand = new RelayCommand<object>(NavigateToSettings);
            NavigateToLogInCommand = new RelayCommand<object>(NavigateToLogIn);
            ExitAppCommand = new RelayCommand<object>(ExitApp);
            NavigateToPickVariantCommand = new RelayCommand<object>(NavigateToPickVariant);
            NavigateToRegisterCommand = new RelayCommand<object>(NavigateToRegister);
        }

        public void NavigateToRegister(object obj)
        {
            _navigationService.NavigateTo("/Register");
        }

        public void ExitApp(object obj)
        {
            Application.Current.Shutdown();
        }

        public void NavigateToPickVariant(object obj)
        {
            _navigationService.NavigateTo("/PickVariant");
        }

        private void LogIn(object obj)
        {
            string username = obj as string;
            if (!string.IsNullOrEmpty(username))
            {
                var userRepository = new UserRepository();
                User dbUser = userRepository.GetUserByUsername(username);

                if (dbUser != null)
                {
                    // assuming the User model has a Name property
                    LoggedInUser = dbUser.Username;
                }
                // ... rest of the code
            }
        }



        private void NavigateToSettings(object obj)
        {
            _navigationService.NavigateTo("/Settings");
        }

        private void NavigateToLogIn(object obj)
        {
            _navigationService.NavigateTo("/LogIn");
        }

        public ObservableCollection<string> Usernames
        {
            get { return _usernames; }
            set
            {
                _usernames = value;
                OnPropertyChanged(nameof(Usernames));
            }
        }

        // Implementace INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}


