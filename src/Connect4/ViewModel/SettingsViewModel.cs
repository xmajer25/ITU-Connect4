using Connect4.BL.Services;
using Connect4.Commands;
using Connect4.DAL.DatabaseHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using NavService = Connect4.Services.NavigationService;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Connect4.ViewModel
{
    public class SettingsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand NavigateToMenuCommand { get; private set; }

        private readonly NavService _navigationService;
        private readonly UserService _userService;  // Make this readonly as well
        private ObservableCollection<string> _usernames;

        public SettingsViewModel()
        {
            DatabaseInitializer.Initialize();
            _navigationService = new NavService();
            _userService = new UserService();

            NavigateToMenuCommand = new RelayCommand<object>(NavigateToMenu);
        }

        public void NavigateToMenu(object obj)
        {
            _navigationService.NavigateTo("/Menu");
        }
    }
}
