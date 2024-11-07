using biorand.app.ViewModels.Game;
using biorand.app.ViewModels.Item;
using biorand.app.ViewModels.Player;
using biorand.app.ViewModels.Seed;
using IntelOrca.Biohazard;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biorand.app.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IAppContext _appContext;
        private ObservableCollection<GameListItemViewModel> _gameListItems = new ObservableCollection<GameListItemViewModel>();
        private GameListItemViewModel _selectedGameListItem;
        private PlayerConfigurationViewModel _playerConfigurationViewModel;
        private SeedConfigurationViewModel _seedConfigurationViewModel;
        private ItemConfigurationViewModel _itemConfigurationViewModel;

        private string _bioRandVersion;

        public MainWindowViewModel(IAppContext appContext)
        {
            _appContext = appContext;

            GameListItems.Add(new GameListItemViewModel() { IsSelected = false, BioVersion = BioVersion.Biohazard1, DisplayName = "Resident Evil 1", ImagePath = "/Resources/Images/RE1/logo.png" });
            GameListItems.Add(new GameListItemViewModel() { IsSelected = true, BioVersion = BioVersion.Biohazard2, DisplayName = "Resident Evil 2", ImagePath = "/Resources/Images/RE2/logo.png" });
            GameListItems.Add(new GameListItemViewModel() { IsSelected = false, BioVersion = BioVersion.Biohazard3, DisplayName = "Resident Evil 3", ImagePath = "/Resources/Images/RE3/logo.png" });
            GameListItems.Add(new GameListItemViewModel() { IsSelected = false, BioVersion = BioVersion.BiohazardCv, DisplayName = "Resident Evil CVX", ImagePath = "/Resources/Images/RECVX/logo.png" });

            PlayerConfigurationViewModel = new PlayerConfigurationViewModel(_appContext) { IsEnabled = true };
            SeedConfigurationViewModel = new SeedConfigurationViewModel(_appContext);
            ItemConfigurationViewModel = new ItemConfigurationViewModel(_appContext);
            BioRandVersion = "UI Revision Branch";

            NavigateUriClickCommand = new DelegateCommand<string>(OnNavigateUriClickCommand);
        }

        public ObservableCollection<GameListItemViewModel> GameListItems { get => _gameListItems; set => SetProperty(ref _gameListItems, value); }
        public GameListItemViewModel SelectedGameListItem { get => _selectedGameListItem; set { SetProperty(ref _selectedGameListItem, value); RefreshControls(); } }
        public PlayerConfigurationViewModel PlayerConfigurationViewModel { get => _playerConfigurationViewModel; set => SetProperty(ref _playerConfigurationViewModel, value); }
        public SeedConfigurationViewModel SeedConfigurationViewModel { get => _seedConfigurationViewModel; set => SetProperty(ref _seedConfigurationViewModel, value); }
        public ItemConfigurationViewModel ItemConfigurationViewModel { get => _itemConfigurationViewModel; set => SetProperty(ref _itemConfigurationViewModel, value); }
        public string BioRandVersion { get => _bioRandVersion; set => SetProperty(ref _bioRandVersion, value); }
        public DelegateCommand<string> NavigateUriClickCommand { get; }


        private void RefreshControls()
        {
            _appContext.SetSelectedVersion(SelectedGameListItem.BioVersion);
            foreach (var g in GameListItems) { g.IsSelected = g.BioVersion == _appContext.SelectedVersion; }
            PlayerConfigurationViewModel.RefreshControls();
            SeedConfigurationViewModel.RefreshControls();
            ItemConfigurationViewModel.RefreshControls();
        }

        private void OnNavigateUriClickCommand(string uri)
        {
            if (uri != null)
            {
                // Handle the hyperlink click event with the NavigateUri
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = uri,
                    UseShellExecute = true
                });
            }
        }
    }
}


