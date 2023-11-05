using Connect4.BL.Services;
using Connect4.Commands;
using Connect4.DAL.DatabaseHelpers;
using Connect4.DAL.DataModels;
using Connect4.ViewModel.Interfaces;
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
    public class ProfileViewModel : ILoadUser, INotifyPropertyChanged
    {
        public User CurrentUser { get; set; }
        public string username { get; set; } = string.Empty;
        private string originalUsername { get; set; } = string.Empty;
        public ICommand NavigateToMenuCommand { get; private set; }
        public ICommand EditNameCommand { get; private set; }
        private readonly NavService _navigationService;
        private readonly UserService _userService;

        public event PropertyChangedEventHandler PropertyChanged;

        public ProfileViewModel()
        {
            DatabaseInitializer.Initialize();
            _navigationService = new NavService();
            _userService = new UserService();
            NavigateToMenuCommand = new RelayCommand<object>(NavigateToMenu);
            EditNameCommand = new RelayCommand<string>(EditName);
        }
        public void LoadUser(User user)
        {
            CurrentUser = user;

            if (CurrentUser != null)
            {
                username = CurrentUser.Username;
                originalUsername = CurrentUser.Username;
            }
        }

        public void EditName(string name)
        {
            if (originalUsername == username) return;
            if (_userService.IsUserNameRegistered(username)) return;
            if(username == string.Empty) return;

            _userService.UpdateUser(CurrentUser.Id, username, CurrentUser.Password, CurrentUser.Email, CurrentUser.GamesPlayed, CurrentUser.GamesWon, CurrentUser.GoldTotal, CurrentUser.GoldActual);
            _navigationService.NavigateTo("/Menu", CurrentUser);
        }

        public void NavigateToMenu(object obj)
        {
            _navigationService.NavigateTo("/Menu", CurrentUser);
        }
    }
}
