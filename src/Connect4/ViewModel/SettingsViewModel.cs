using Connect4.BL.Services;
using Connect4.Commands;
using Connect4.DAL.DatabaseHelpers;
using System;
using System.ComponentModel;
using System.Windows.Input;
using Connect4.DAL.DataModels;
using Connect4.ViewModel.Interfaces;
using Connect4.Services;
using System.Runtime.CompilerServices;

namespace Connect4.ViewModel
{
    public class SettingsViewModel : INotifyPropertyChanged, ILoadUser
    {
        public User CurrentUser { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        private NavigationService _navigationService;
        private UserService _userService;
        private SettingsService _settingsService;
        private Settings _currentSettings;

        public ICommand SaveSettingsCommand { get; private set; }
        public ICommand NavigateToMenuCommand { get; private set; }

        public SettingsViewModel()
        {
            DatabaseInitializer.Initialize();
            _navigationService = new NavigationService();
            _userService = new UserService();
            _settingsService = new SettingsService(CurrentUser?.Id ?? default); // Initial setup
            SaveSettingsCommand = new RelayCommand<object>(SaveSettings);
            NavigateToMenuCommand = new RelayCommand<object>(NavigateToMenu);
             
            LoadSettings();
        }

        public bool IsNarrationEnabled
        {
            get => _currentSettings?.IsNarrationEnabled ?? false;
            set
            {
                if (_currentSettings.IsNarrationEnabled != value)
                {
                    _currentSettings.IsNarrationEnabled = value;
                    RaisePropertyChanged();
                }
            }
        }

        private void LoadSettings()
        {
            _currentSettings = _settingsService.LoadSettings();
            RaisePropertyChanged(nameof(IsNarrationEnabled));
        }

        private void SaveSettings(object obj)
        {
            _settingsService.SaveSettings(_currentSettings);
            _navigationService.NavigateTo("/Menu", CurrentUser);
        }

        public void LoadUser(User user)
        {
            CurrentUser = user;
            _settingsService = new SettingsService(CurrentUser.Id); // Reassign with new user ID
            LoadSettings();
        }

        public void NavigateToMenu(object obj)
        {
            _navigationService.NavigateTo("/Menu", CurrentUser);
        }

        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
