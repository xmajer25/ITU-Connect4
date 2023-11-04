using Connect4.BL.Services;
using Connect4.Commands;
using Connect4.DAL.DatabaseHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using NavService = Connect4.Services.NavigationService;

namespace Connect4.ViewModel
{
    public class PickVariantViewModel
    {
        public ICommand NavigateToMenuCommand { get; private set; }
        public ICommand NavigateToStandardModeCommand { get; private set; }
        public ICommand NavigateToCrazyHouseModeCommand { get; private set; }
        private readonly NavService _navigationService;

        public PickVariantViewModel()
        {
            DatabaseInitializer.Initialize();
            _navigationService = new NavService();
            NavigateToMenuCommand = new RelayCommand<object>(NavigateToMenu);
            NavigateToStandardModeCommand = new RelayCommand<object>(NavigateToStandardMode);
            NavigateToCrazyHouseModeCommand = new RelayCommand<object>(NavigateToCrazyHouseMode);
        }

        public void NavigateToCrazyHouseMode(object obj)
        {
            _navigationService.NavigateTo("/CrazyHouseMode");
        }

        public void NavigateToMenu(object obj)
        {
            _navigationService.NavigateTo("/Menu");
        }

        public void NavigateToStandardMode(object obj)
        {
            _navigationService.NavigateTo("/StandardMode");
        }
    }
}
