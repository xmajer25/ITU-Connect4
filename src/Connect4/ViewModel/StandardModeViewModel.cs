using Connect4.BL.Services;
using Connect4.BL.Services.GridServices;
using Connect4.Commands;
using Connect4.DAL.DatabaseHelpers;
using Connect4.DAL.DataModels;
using Connect4.DAL.Repositories;
using Connect4.DAL.Repositories.Interfaces;
using Connect4.ViewModel.Interfaces;
using Connect4.Views.PopUps;
using Connect4.ViewUserControl;
using System;
using System.ComponentModel;
using System.Speech.Synthesis;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using NavService = Connect4.Services.NavigationService;

/*
 * Author   : Dušan Slúka (xsluka00)
 * File     : StandardModeViewModel
 * Brief    : ViewModel for standard game mode view. 
 */
namespace Connect4.ViewModel
{
    public class StandardModeViewModel : ILoadUser, INotifyPropertyChanged
    {
        public User CurrentUser { get; set; }
        public GridModelStandard GridModelStandard { get; set; }
        private readonly NavService _navigationService;
        private GridStandardService _gridService;
        private GridBaseService _gridBaseService;
        private IGridStandardRepository _gridRepository;
        private  SettingsService _settingsService;
        private readonly UserService _userService;
        private readonly UserCustomizableService _userCustomizableService;
        private readonly UserAchievementService _userAchievementService;
        private SpeechSynthesizer synthesizer;

        public Grid MainGrid;
        public Grid GameGrid;

        public Canvas TopCanvas;
        public Canvas BottomCanvas;

        public ICommand NavigateToPickVariantCommand { get; private set; }
        public ICommand PlaceBallCommand { get; private set; }

        private bool IsAnimationOn { get; set; } = false;
        private readonly string _player1Turn = "Player1 Turn";
        private readonly string _player2Turn = "Player2 Turn";
        private string _playerTurn;

        public event PropertyChangedEventHandler PropertyChanged;

        public string PlayerTurn
        {
            get { return _playerTurn; }
            set
            {
                if (_playerTurn != value)
                {
                    _playerTurn = value;
                    OnPropertyChanged("PlayerTurn");
                }
            }
        }


        private string token1 = "/Resources/Images/Tokens/TokenBlue.png";
        private string token2 = "/Resources/Images/Tokens/TokenRed.png";
        private string _tokenSkin = "/Resources/Images/Tokens/TokenBlue.png";

        public string TokenSkin
        {
            get { return _tokenSkin; }
            set
            {
                if (_tokenSkin != value)
                {
                    _tokenSkin = value;
                    OnPropertyChanged("TokenSkin");
                }
            }
        }

        public void LoadUser(User user)
        {
            CurrentUser = user;
            if (user != null)
            {
                _settingsService = new SettingsService(CurrentUser.Id);
                SetupNarration();
            }
            else { 
            }

        }

        public StandardModeViewModel()
        {
            _navigationService = new NavService();
            _gridRepository = new GridStandardRepository(); // Instantiate the concrete repository
            _gridService = new GridStandardService(_gridRepository); // Pass the repository to the service
            GridModelStandard = new GridModelStandard();
            _userService = new UserService();
            _gridBaseService = new GridBaseService();


            NavigateToPickVariantCommand = new RelayCommand<object>(NavigateToPickVariant);

            PlaceBallCommand = new RelayCommand<String>(PlaceBall);
            
            _tokenSkin = token1;
            PlayerTurn = _player1Turn;

            CommandManager.InvalidateRequerySuggested();
        }

        

        private void SetupNarration()
        {
            

            var settings = _settingsService.LoadSettings();

            if (settings.IsNarrationEnabled == true)
            {
                synthesizer = new SpeechSynthesizer();
                synthesizer.SetOutputToDefaultAudioDevice();
            }
            else if (settings.IsNarrationEnabled == false)
            {

                synthesizer = null;
            }
        }

        public void HandleKeyPress(Key key)
        {
            int column = 0;

            if (key >= Key.D1 && key <= Key.D9)
            {
                column = key - Key.D1 + 2  ; // For top row numbers
            }
            else if (key >= Key.NumPad1 && key <= Key.NumPad9)
            {
                column = key - Key.NumPad1 + 2; // For numeric keypad numbers
            }

            if (column >= 1 && column <= 9)
            {
                PlaceBall(column.ToString());
            }
        }

        private void AnnouncePlayerSwitch()
        {
            string announcement = PlayerTurn == _player1Turn ? "Player 1's turn" : "Player 2's turn";
            if (synthesizer != null)
            {
                synthesizer.SpeakAsync(announcement); // Speak only if synthesizer is not null
            }
        }

        private void AnnounceTokenPlacement(int column, int row)
        {
            string announcement = $"Token placed at column {column}, {row} tokens high.";
            if (synthesizer != null)
            {
                synthesizer.SpeakAsync(announcement); // Speak only if synthesizer is not null
            }
        }

        public void SwapPlayerTurn()
        {
            PlayerTurn = (PlayerTurn == _player1Turn ? _player2Turn : _player1Turn);
            AnnouncePlayerSwitch(); // Announce the player switch
        }

        public void SwapTokenSkins()
         => TokenSkin = (TokenSkin == token1 ? token2 : token1);


        public double GetColumnPosition(int index, Grid grid)
        {
            double columnXPosition = 0;
            int i = 0;
            for (; i < index; i++)
            {
                columnXPosition += GetCurrentColumnWidth(i, grid);
            }
            columnXPosition += GetCurrentColumnWidth(i, grid) / 4;
            return columnXPosition;
        }

        public double GetCurrentColumnWidth(int index, Grid grid)
            => grid.ColumnDefinitions[index].ActualWidth;


        public double GetRowPosition(int index, Grid grid)
        {
            double columnYPosition = 0;
            int i = 0;
            for (; i < index; i++)
            {
                columnYPosition += GetCurrentRowHeight(i, grid);
            }
            columnYPosition += GetCurrentRowHeight(i, grid) / 4;
            return columnYPosition;
        }

        public double GetCurrentRowHeight(int index, Grid grid)
            => grid.RowDefinitions[index].ActualHeight;

        public void PlaceBall(String param)
        {
            int column = Convert.ToInt32(param);
            if (IsAnimationOn) return;
            IsAnimationOn = true;

            // Make a move in the game logic
            CellState currentPlayer = (PlayerTurn == _player1Turn) ? CellState.Player1 : CellState.Player2;
            bool moveSuccessful = _gridService.MakeMove(column - 2, currentPlayer);
            if (!moveSuccessful)
            {
                IsAnimationOn = false;
                return; // The column is full or some other reason the move is not valid.
            }

            // Successful move
            int row = _gridService.GetFirstEmpty(column - 2)+1; // Calculate the row where the token is placed

            AnnounceTokenPlacement(column-1, row);

            // Create a new ball control
            BallControl ball = new BallControl();
            double startXPosition = GetColumnPosition(column, GameGrid);
            double startYPosition = 0; // The initial Y position of the ball (usually at the top of the canvas)
            double endYPosition = GetRowPosition(_gridService.GetMaxRow(column - 2) + 3, GameGrid);

            // Ensure startYPosition and endYPosition are valid to avoid NaN
            if (double.IsNaN(startYPosition) || double.IsNaN(endYPosition))
            {
                // Handle invalid positions (perhaps log an error or set default positions)
                return;
            }

            // Calculate animation duration based on distance
            double distance = Math.Abs(endYPosition - startYPosition);
            TimeSpan duration = TimeSpan.FromSeconds(Math.Max(distance / 400, 0.1)); // Ensure duration is not zero

            // Set initial position of the ball
            Canvas.SetLeft(ball, startXPosition);
            Canvas.SetTop(ball, startYPosition);

            // Animation for dropping the ball
            DoubleAnimation animation = new DoubleAnimation
            {
                To = endYPosition,
                Duration = duration,
                FillBehavior = FillBehavior.Stop
            };

            ball.Visibility = Visibility.Visible;

            Storyboard.SetTarget(animation, ball);
            Storyboard.SetTargetProperty(animation, new PropertyPath(Canvas.TopProperty));

            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(animation);

            storyboard.Completed += (sender, e) =>
            {
                FinalizeBallPlacement(ball, endYPosition, startXPosition);
                GridModelStandard = _gridService.GetGridModel();
                ProcessGameStateChanges(); // Process the game state changes after the animation
            };

            BottomCanvas.Children.Add(ball);
            storyboard.Begin();
        }

        private void FinalizeBallPlacement(BallControl ball, double endYPosition, double startingX)
        {
            // Fix token position
            ball.SetValue(Canvas.TopProperty, endYPosition+1);
            ball.SetValue(Canvas.LeftProperty, startingX - 3);

            // Set token skin statically
            Image ballImage = ball.FindName("Token") as Image;
            BindingOperations.ClearBinding(ballImage, Image.SourceProperty);
            BitmapImage bitmapImage = new BitmapImage(new Uri(TokenSkin, UriKind.RelativeOrAbsolute));
            ballImage.Source = bitmapImage;
        }

        private void ProcessGameStateChanges()
        {
            // Check for win or draw using the service
            if (_gridService.CheckWinCondition(GridModelStandard))
            {
                int winner = _gridBaseService.GetPlayer();
                
                if (synthesizer != null)
                {
                    synthesizer.SpeakAsyncCancelAll(); // Cancel any ongoing speech
                    string winAnnouncement = winner == 1 ? "Player 1 wins!" : "Player 2 wins!";
                    synthesizer.SpeakAsync(winAnnouncement); // Announce the winner
                }
                _navigationService.NavigateTo("/EndScreen", CurrentUser, winner);
            }
            else if (_gridService.IsBoardFull(GridModelStandard))
            {
                // Handle draw scenario
                
                if (synthesizer != null)
                {
                    synthesizer.SpeakAsyncCancelAll(); // Cancel any ongoing speech
                    string drawAnnouncement = "The game ended in a draw.";
                    synthesizer.SpeakAsync(drawAnnouncement); // Announce the draw
                }
                _navigationService.NavigateTo("/EndScreen", CurrentUser, 0);
            }


            // Swap players and skins for the next move
            SwapTokenSkins();
            SwapPlayerTurn();

            // Allow the next move
            IsAnimationOn = false;
        }

        public void NavigateToPickVariant(object obj)
        {
            ExitGamePopUp exitGamePopUp = new ExitGamePopUp();
            bool? result = exitGamePopUp.ShowDialog();
            if (result == true)
            {
                if (synthesizer != null)
                {
                    synthesizer.SpeakAsyncCancelAll();
                }
                _navigationService.NavigateTo("/PickVariant", CurrentUser);
            }

        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
