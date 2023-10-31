using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;
using Connect4.Commands;
using NavService = Connect4.Services.NavigationService;


namespace Connect4.ViewModel
{
    public class MenuViewModel : INotifyPropertyChanged
    {
        public ICommand NavigateToSettingsCommand { get; }

        // Step 1: Create a private field for NavigationService
        private readonly NavService _navigationService;

        public MenuViewModel()
        {
            // Step 2: Initialize the NavigationService field
            _navigationService = new NavService();

            NavigateToSettingsCommand = new RelayCommand(async () => await NavigateToSettings());
        }

        private async Task NavigateToSettings()
        {
            // Step 3: Use the instance to call NavigateTo
            _navigationService.NavigateTo("Views/Settings.xaml");        
        }

        // Implementace INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

