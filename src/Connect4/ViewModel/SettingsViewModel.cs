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

/*
 * Author   : Dušan Slúka (xsluka00)
 * File     : SettingsViewModel
 * Brief    : SettingsViewModel for settings in app.
 */
namespace Connect4.ViewModel
{
    public class SettingsViewModel : INotifyPropertyChanged, ILoadUser
    {
        public User CurrentUser { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly NavigationService _navigationService;
        private readonly UserService _userService;
        private SettingsService _settingsService;
        private Settings _currentSettings;

        public ICommand SaveSettingsCommand { get; private set; }
        public ICommand NavigateToMenuCommand { get; private set; }

        public SettingsViewModel()
        {
            _navigationService = new NavigationService();
            _userService = new UserService();
            SaveSettingsCommand = new RelayCommand<object>(SaveSettings);
            NavigateToMenuCommand = new RelayCommand<object>(NavigateToMenu);

            // Initialize _currentSettings with default values
            _currentSettings = new Settings { IsNarrationEnabled = false };
        }

        private SettingsService SettingsService
        {
            get
            {
                if (_settingsService == null)
                {
                    // Initialize _settingsService when it's first needed
                    _settingsService = new SettingsService(CurrentUser?.Id ?? default);
                }
                return _settingsService;
            }
        }

        public bool IsNarrationEnabled
        {
            get => _currentSettings?.IsNarrationEnabled ?? false;
            set
            {
                if (_currentSettings?.IsNarrationEnabled != value)
                {
                    _currentSettings.IsNarrationEnabled = value;
                    RaisePropertyChanged();
                }
            }
        }

        public int MasterVolume
        {
            get => _currentSettings?.MasterVolume ?? 0;
            set
            {
                if (_currentSettings?.MasterVolume != value)
                {
                    _currentSettings.MasterVolume = value;
                    RaisePropertyChanged();
                }
            }
        }

        public int EffectVolume
        {
            get => _currentSettings?.EffectVolume ?? 0;
            set
            {
                if (_currentSettings?.EffectVolume != value)
                {
                    _currentSettings.EffectVolume = value;
                    RaisePropertyChanged();
                }
            }
        }


        private void LoadSettings()
        {
            try
            {
                _currentSettings = SettingsService.LoadSettings();
                RaisePropertyChanged(nameof(IsNarrationEnabled));
                RaisePropertyChanged(nameof(MasterVolume));
                RaisePropertyChanged(nameof(EffectVolume));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Loading settings: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
            }
        }

        private void SaveSettings(object obj)
        {
            try
            {
                SettingsService.SaveSettings(_currentSettings);
                ApplyAudioSettings(); // Apply the audio settings after saving
                _navigationService.NavigateTo("/Menu", CurrentUser);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving settings: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
            }
        }

        private void ApplyAudioSettings()
        {
            AudioManager.SetVolume(_currentSettings.MasterVolume / 100.0);
        }

        public void LoadUser(User user)
        {
            CurrentUser = user;
            LoadSettings();
            ApplyAudioSettings(); // Apply audio settings when the user is loaded
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
