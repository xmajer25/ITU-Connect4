using Connect4.BL.Services;
using Connect4.Commands;
using Connect4.DAL.DatabaseHelpers;
using Connect4.DAL.DataModels;
using Connect4.ViewModel.Interfaces;
using System.ComponentModel;
using System.Windows.Input;
using NavService = Connect4.Services.NavigationService;

namespace Connect4.ViewModel
{
    public class ProfileViewModel : ILoadUser, INotifyPropertyChanged
    {
        public User CurrentUser { get; set; }

        private string _username = string.Empty;
        public string username
        {
            get
            {
                return _username;
            }
            set
            {
                if (_username != value)
                {
                    _username = value;
                    OnPropertyChanged("username");
                }
            }
        }
        private string originalUsername { get; set; } = string.Empty;


        private string _email = string.Empty;
        public string email
        {
            get
            {
                return _email;
            }
            set
            {
                if (_email != value)
                {
                    _email = value;
                    OnPropertyChanged("email");
                }
            }
        }
        private string originalEmail { get; set; } 

        
        public ICommand NavigateToMenuCommand { get; private set; }
        public ICommand EditNameCommand { get; private set; }
        public ICommand EditEmailCommand { get; private set; }
        private readonly NavService _navigationService;
        private readonly UserService _userService;

        public event PropertyChangedEventHandler PropertyChanged;

        public ProfileViewModel()
        {
            DatabaseInitializer.Initialize();
            _navigationService = new NavService();
            _userService = new UserService();
            NavigateToMenuCommand = new RelayCommand<object>(NavigateToMenu);
            EditNameCommand = new RelayCommand<object>(EditName);
            EditEmailCommand = new RelayCommand<object>(EditEmail);
        }
        public void LoadUser(User user)
        {
            CurrentUser = user;

            if (CurrentUser != null)
            {
                username = CurrentUser.Username;
                originalUsername = CurrentUser.Username;
                originalEmail = email = CurrentUser.Email;
            }
        }
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void EditName(object obj)
        {
            if (originalUsername == username) return;
            if (_userService.IsUserNameRegistered(username)) return;
            if(username == string.Empty) return;

            _userService.UpdateUser(CurrentUser.Id, username, CurrentUser.Password, CurrentUser.Email, CurrentUser.GamesPlayed, CurrentUser.GamesWon, CurrentUser.GoldTotal, CurrentUser.GoldActual);
            CurrentUser.Username = username;
        }

        public void EditEmail(object obj)
        {
            if(email == string.Empty) return;
            if (originalEmail == email) return;
            if(_userService.IsUserEmailRegistered(email)) return;

            _userService.UpdateUser(CurrentUser.Id, CurrentUser.Username, CurrentUser.Password, email, CurrentUser.GamesPlayed, CurrentUser.GamesWon, CurrentUser.GoldTotal, CurrentUser.GoldActual);
            CurrentUser.Email = email;
        }

        public void NavigateToMenu(object obj)
        {
            _navigationService.NavigateTo("/Menu", CurrentUser);
        }
    }
}
