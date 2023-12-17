using Connect4.Commands;
using Connect4.DAL.DatabaseHelpers;
using Connect4.DAL.DataModels;
using Connect4.ViewModel.Interfaces;
using Connect4.BL.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using NavService = Connect4.Services.NavigationService;
using System.Collections.ObjectModel;

namespace Connect4.ViewModel
{
    public class ShopViewModel : INotifyPropertyChanged, ILoadUser
    {
        public User CurrentUser { get; set; }

        private string _username = string.Empty;
        private int _gold = 0;

        private bool _shopTokens = false;
        private bool _shopAvatars = false;
        private bool _shopBckgs = true;

        /* COMMANDS */
        public ICommand NavigateToMenuCommand { get; private set; }
        public ICommand SwitchTokensCommand { get; private set; }
        public ICommand SwitchAvatarsCommand { get; private set; }
        public ICommand SwitchBckgsCommand { get; private set; }

        /* SERVICES */
        private readonly NavService _navigationService;
        private readonly CustomizableService _customService;
        private readonly UserCustomizableService _userCustomizableService;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<string> _owned {  get; set; }
        public ObservableCollection<string> _selected { get; set; }
        public ObservableCollection<string> _purchasable { get; set; }
        public ObservableCollection<string> _notPurchasable { get; set; }

        public ShopViewModel()
        {
            DatabaseInitializer.Initialize();
            _navigationService = new NavService();
            _customService = new CustomizableService();
            _userCustomizableService = new UserCustomizableService();
            NavigateToMenuCommand = new RelayCommand<object>(NavigateToMenu);
            SwitchTokensCommand = new RelayCommand<object>(SetTokens);
            SwitchAvatarsCommand = new RelayCommand<object>(SetAvatars);
            SwitchBckgsCommand = new RelayCommand<object>(SetBckgs);

        }

        /* Load user */
        public void LoadUser(User user)
        {
            CurrentUser = user;

            if (CurrentUser != null)
            {
                _username = CurrentUser.Username;
                _gold = CurrentUser.GoldActual;
            }
        }

        public bool ShopTokens
        {
            get { return _shopTokens; }
            set
            {
                if (_shopTokens != value)
                {
                    _shopTokens = value;
                    OnPropertyChanged("ShopTokens");
                }
            }
        }

        public bool ShopAvatars
        {
            get { return _shopAvatars; }
            set
            {
                if (_shopAvatars != value)
                {
                    _shopAvatars = value;
                    OnPropertyChanged("ShopAvatars");
                }
            }
        }

        public bool ShopBckgs
        {
            get { return _shopBckgs; }
            set
            {
                if (_shopBckgs != value)
                {
                    _shopBckgs = value;
                    OnPropertyChanged("ShopBckgs");
                }
            }
        }

        public void LoadCustomizables()
        {
            if(ShopBckgs == true)
            {
                _owned = _customService.GetCustomizablesForUser(CurrentUser.Id, 0, 0);
                _selected = _customService.GetCustomizablesForUser(CurrentUser.Id, 1, 0);
                if(CurrentUser.GoldTotal >= 2000)
                {
                    _purchasable = _customService.GetAvailableCustomizables(CurrentUser.Id, 0);
                    _notPurchasable = null;
                }
                else
                {
                    _purchasable = null;
                    _notPurchasable = _customService.GetAvailableCustomizables(CurrentUser.Id, 0);

                }

            }else if(ShopAvatars == true)
            {
                _owned = _customService.GetCustomizablesForUser(CurrentUser.Id, 0, 1);
                _selected = _customService.GetCustomizablesForUser(CurrentUser.Id, 1, 1);
                if (CurrentUser.GoldTotal >= 2000)
                {
                    _purchasable = _customService.GetAvailableCustomizables(CurrentUser.Id, 1);
                    _notPurchasable = null;
                }
                else
                {
                    _purchasable = null;
                    _notPurchasable = _customService.GetAvailableCustomizables(CurrentUser.Id, 1);

                }

            }
            else if(ShopTokens == true)
            {
                _owned = _customService.GetCustomizablesForUser(CurrentUser.Id, 0, 2);
                _selected = _customService.GetCustomizablesForUser(CurrentUser.Id, 1, 2);
                if (CurrentUser.GoldTotal >= 1000)
                {
                    _purchasable = _customService.GetAvailableCustomizables(CurrentUser.Id, 2);
                    _notPurchasable = null;
                }
                else
                {
                    _purchasable = null;
                    _notPurchasable = _customService.GetAvailableCustomizables(CurrentUser.Id, 2);

                }
            }
        }

        public void SetAvatars(object obj) 
        {
            ShopAvatars = true;
            ShopBckgs = false;
            ShopTokens = false;
            LoadCustomizables();
        }

        public void SetBckgs(object obj)
        {
            ShopAvatars = false;
            ShopBckgs = true;
            ShopTokens = false;
            LoadCustomizables();
        }

        public void SetTokens(object obj)
        {
            ShopAvatars = false;
            ShopBckgs = false;
            ShopTokens = true;
            LoadCustomizables();
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void NavigateToMenu(object obj)
        {
            _navigationService.NavigateTo("/Menu", CurrentUser);
        }
    }
}
