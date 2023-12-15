using Connect4.BL.Services;
using Connect4.Commands;
using Connect4.DAL.DatabaseHelpers;
using Connect4.DAL.DataModels;
using Connect4.ViewModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using NavService = Connect4.Services.NavigationService;

/*
 * Author   : Jakub Majer (xmajer25)
 * File     : PickVariantViewModel
 * Brief    : Navigation page. Allows user to pick from different game modes.
 */

namespace Connect4.ViewModel
{
    public class PickVariantViewModel : ILoadUser
    {
        public User CurrentUser { get; set; }

        /* COMMANDS */
        public ICommand NavigateToMenuCommand { get; private set; }
        public ICommand NavigateToStandardModeCommand { get; private set; }
        public ICommand NavigateToCrazyHouseModeCommand { get; private set; }

        /* SERVICES */
        private readonly NavService _navigationService;

        public PickVariantViewModel()
        {
            DatabaseInitializer.Initialize();
            _navigationService = new NavService();
            NavigateToMenuCommand = new RelayCommand<object>(NavigateToMenu);
            NavigateToStandardModeCommand = new RelayCommand<object>(NavigateToStandardMode);
            NavigateToCrazyHouseModeCommand = new RelayCommand<object>(NavigateToCrazyHouseMode);
        }

        /* Load logged in user */
        public void LoadUser(User user)
        {
            CurrentUser = user;
        }

        /* Navigation -> go to crazy house mode */
        public void NavigateToCrazyHouseMode(object obj)
        {
            _navigationService.NavigateTo("/CrazyHouseMode", CurrentUser);
        }

        /* Navigation -> go to main menu screen*/
        public void NavigateToMenu(object obj)
        {
            _navigationService.NavigateTo("/Menu", CurrentUser);
        }

        /* Navigation -> go to standard mode*/
        public void NavigateToStandardMode(object obj)
        {
            _navigationService.NavigateTo("/StandardMode", CurrentUser);
        }
    }
}
