using Connect4.BL.Services;
using Connect4.Commands;
using Connect4.DAL.DatabaseHelpers;
using Connect4.DAL.DataModels;
using Connect4.ViewModel.Interfaces;
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
    public class StandardModeViewModel : ILoadUser
    {
        public User CurrentUser { get; set; }
        private readonly NavService _navigationService;
        public ICommand NavigateToPickVariantCommand { get; private set; }

        public StandardModeViewModel()
        {
            DatabaseInitializer.Initialize();
            _navigationService = new NavService();

            NavigateToPickVariantCommand = new RelayCommand<object>(NavigateToPickVariant);
        }
        public void LoadUser(User user)
        {
            CurrentUser = user;
        }

        public void NavigateToPickVariant(object obj)
        {
            _navigationService.NavigateTo("/PickVariant", CurrentUser);
        }
    }
}
