using Connect4.Commands;
using Connect4.DAL.DatabaseHelpers;
using System.Windows.Documents;
using System.Windows.Input;
using NavService = Connect4.Services.NavigationService;

namespace Connect4.ViewModel
{
    public class CrazyHouseViewModel
    {
        private readonly NavService _navigationService;
        public ICommand NavigateToPickVariantCommand { get; private set; }

        public CrazyHouseViewModel()
        {
            DatabaseInitializer.Initialize();
            _navigationService = new NavService();

            NavigateToPickVariantCommand = new RelayCommand<object>(NavigateToPickVariant);
        }

        public void NavigateToPickVariant(object obj)
        {
            _navigationService.NavigateTo("/PickVariant");
        }
    }
}
