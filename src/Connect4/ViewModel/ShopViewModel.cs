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
        private string _token2 = string.Empty;

        private bool _shopTokens = false;
        private bool _shopAvatars = false;
        private bool _shopBckgs = true;

        /* COMMANDS */
        public ICommand NavigateToMenuCommand { get; private set; }
        public ICommand SwitchTokensCommand { get; private set; }
        public ICommand SwitchAvatarsCommand { get; private set; }
        public ICommand SwitchBckgsCommand { get; private set; }
        public ICommand SetSelectedCommand { get; private set; }
        public ICommand BuySkinCommand { get; private set; }
        public ICommand SetTokenCommnad {  get; private set; }
        public ICommand SetToken2Commnad {  get; private set; }

        /* SERVICES */
        private readonly NavService _navigationService;
        private readonly CustomizableService _customService;
        private readonly UserCustomizableService _userCustomizableService;
        private readonly UserService _userService;

        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<string> _owned {  get; set; }
        private ObservableCollection<string> _selected { get; set; }
        private ObservableCollection<string> _purchasable { get; set; }
        private ObservableCollection<string> _notPurchasable { get; set; }

        public ObservableCollection<string> Owned 
        {
            get { return _owned; }
            set
            {
                if(_owned != value)
                {
                    _owned = value;
                    OnPropertyChanged(nameof(Owned));
                }
            } 
        }

        public ObservableCollection<string> Selected
        {
            get { return _selected; }
            set
            {
                if (_selected != value)
                {
                    _selected = value;
                    OnPropertyChanged(nameof(Selected));
                }
            }
        }

        public ObservableCollection<string> Purchasable
        {
            get { return _purchasable; }
            set
            {
                if (_purchasable != value)
                {
                    _purchasable = value;
                    OnPropertyChanged(nameof(Purchasable));
                }
            }
        }

        public ObservableCollection<string> NotPurchasable
        {
            get { return _notPurchasable; }
            set
            {
                if (_notPurchasable != value)
                {
                    _notPurchasable = value;
                    OnPropertyChanged(nameof(NotPurchasable));
                }
            }
        }

        public int Gold
        {
            get { return _gold; }
            set
            {
                if (_gold != value)
                {
                    _gold = value;
                    OnPropertyChanged(nameof(Gold));
                }
            }
        }

        public string Token2
        {
            get { return _token2; }
            set
            {
                if (_token2 != value)
                {
                    _token2 = value;
                    OnPropertyChanged(nameof(Token2));
                }
            }
        }


        public ShopViewModel()
        {
            _navigationService = new NavService();
            _customService = new CustomizableService();
            _userCustomizableService = new UserCustomizableService();
            _userService = new UserService();
            NavigateToMenuCommand = new RelayCommand<object>(NavigateToMenu);
            SwitchTokensCommand = new RelayCommand<object>(SetTokens);
            SwitchAvatarsCommand = new RelayCommand<object>(SetAvatars);
            SwitchBckgsCommand = new RelayCommand<object>(SetBckgs);
            SetSelectedCommand = new RelayCommand<string>(SetSelected);
            BuySkinCommand = new RelayCommand<string>(BuySkin);
            SetTokenCommnad = new RelayCommand<string>(SetToken);
            SetToken2Commnad =  new RelayCommand<string>(SetToken2);
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

        public void PageLoaded()
            => LoadCustomizables();

        public void LoadCustomizables()
         {
            if(_shopBckgs == true)
            {
                Owned = _customService.GetCustomizablesForUser(CurrentUser.Id, 0, 0);
                Selected = _customService.GetCustomizablesForUser(CurrentUser.Id, 1, 0);
                if(CurrentUser.GoldTotal >= 2000)
                {
                    Purchasable = _customService.GetAvailableCustomizables(CurrentUser.Id, 0);
                    NotPurchasable = null;
                }
                else
                {
                    Purchasable = null;
                    NotPurchasable = _customService.GetAvailableCustomizables(CurrentUser.Id, 0);

                }

            }else if(ShopAvatars == true)
            {
                Owned = _customService.GetCustomizablesForUser(CurrentUser.Id, 0, 2);
                Selected = _customService.GetCustomizablesForUser(CurrentUser.Id, 1, 2);
                if (CurrentUser.GoldTotal >= 2000)
                {
                    Purchasable = _customService.GetAvailableCustomizables(CurrentUser.Id, 2);
                    NotPurchasable = null;
                }
                else
                {
                    Purchasable = null;
                    NotPurchasable = _customService.GetAvailableCustomizables(CurrentUser.Id, 2);
                }

            }
            else if(ShopTokens == true)
            {
                Owned = _customService.GetCustomizablesForUser(CurrentUser.Id, 0, 1);
                Selected = _customService.GetCustomizablesForUser(CurrentUser.Id, 1, 1);
                Token2 = _customService.GetCustomizablesForUser(CurrentUser.Id, 2, 1)[0];
                if (CurrentUser.GoldTotal >= 1000)
                {
                    Purchasable = _customService.GetAvailableCustomizables(CurrentUser.Id, 1);
                    NotPurchasable = null;
                }
                else
                {
                    Purchasable = null;
                    NotPurchasable = _customService.GetAvailableCustomizables(CurrentUser.Id, 1);
                }
            }
        }

        // selecting customizable
        public void SetSelected(string ImagePath)
        {   
            if(ShopAvatars) _userCustomizableService.SetSelected(CurrentUser.Id, ImagePath, 1, false);
            else if (ShopBckgs) _userCustomizableService.SetSelected(CurrentUser.Id, ImagePath, 0, false);

            LoadCustomizables();
        }

        // buying new customizable
        public void BuySkin(string ImagePath) {
            if(ShopAvatars || ShopBckgs) Gold = _userService.UpdateGold(CurrentUser.Id, Gold - 2000);
            else Gold =  _userService.UpdateGold(CurrentUser.Id, Gold - 1000);

            _userCustomizableService.SaveUserCustom(CurrentUser.Id, ImagePath, 0);
            LoadCustomizables();
        }

        // for selecting Token 1
        public void SetToken(string ImagePath)
        {
            _userCustomizableService.SetSelected(CurrentUser.Id, ImagePath, 2, false);
            LoadCustomizables();
        }

        // for selecting token 2
        public void SetToken2(string ImagePath)
        {
            _userCustomizableService.SetSelected(CurrentUser.Id, ImagePath, 2, true);
            LoadCustomizables();
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
