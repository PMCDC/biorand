using biorand.desktop.Enums;
using biorand.desktop.Factories;
using biorand.desktop.Services;
using IntelOrca.Biohazard;
using IntelOrca.Biohazard.BioRand;
using System.Collections.ObjectModel;
using System.IO;

namespace biorand.desktop.ViewModels.Player;

public class PlayerConfigurationViewModel : BindableBase
{
    private readonly IAppContext _appContext;

    private bool _isEnabled;
    private bool _isSwapCharacterEnabled;
    private bool _isSecondPlayerAvailable;
    private ObservableCollection<PlayerListItemViewModel> _player0Informations = new ObservableCollection<PlayerListItemViewModel>();
    private ObservableCollection<PlayerListItemViewModel> _player1Informations = new ObservableCollection<PlayerListItemViewModel>();
    private PlayerListItemViewModel? _selectedPlayer0Information;
    private PlayerListItemViewModel? _selectedPlayer1Information;

    private PlayerListItemViewModel? _mainPlayer0Information;
    private PlayerListItemViewModel? _mainPlayer1Information;

    public PlayerConfigurationViewModel(IAppContext appContext)
    {
        _appContext = appContext;
        RefreshControls();
    }

    public bool IsEnabled { get => _isEnabled; set => SetProperty(ref _isEnabled, value); }
    public bool IsSwapCharacterEnabled { get => _isSwapCharacterEnabled; set => SetProperty(ref _isSwapCharacterEnabled, value); }
    public bool IsSecondPlayerAvailable { get => _isSecondPlayerAvailable; set => SetProperty(ref _isSecondPlayerAvailable, value); }
    public ObservableCollection<PlayerListItemViewModel> Player0Informations { get => _player0Informations; set => SetProperty(ref _player0Informations, value); }
    public ObservableCollection<PlayerListItemViewModel> Player1Informations { get => _player1Informations; set => SetProperty(ref _player1Informations, value); }
    public PlayerListItemViewModel? SelectedPlayer0Information { get => _selectedPlayer0Information; set => SetProperty(ref _selectedPlayer0Information, value); }
    public PlayerListItemViewModel? SelectedPlayer1Information { get => _selectedPlayer1Information; set => SetProperty(ref _selectedPlayer1Information, value); }
    public PlayerListItemViewModel? MainPlayer0Information { get => _mainPlayer0Information; set => SetProperty(ref _mainPlayer0Information, value); }
    public PlayerListItemViewModel? MainPlayer1Information { get => _mainPlayer1Information; set => SetProperty(ref _mainPlayer1Information, value); }

    public void RefreshControls()
    {
        Player0Informations.Clear();
        Player0Informations.Add(GetRandomPlayerInformation(_appContext.SelectedVersion));
        Player0Informations.AddRange(GetPlayerInformations(_appContext.SelectedRandomizer, Enums.PlayerIndex.Player0));
        SelectedPlayer0Information = Player0Informations.FirstOrDefault(x => string.Equals(x.Name, _appContext.SelectedRandomizer.GetPlayerName((int)Enums.PlayerIndex.Player0), StringComparison.InvariantCultureIgnoreCase));

        Player1Informations.Clear();
        IsSecondPlayerAvailable = _appContext.SelectedVersion == BioVersion.Biohazard1 || _appContext.SelectedVersion == BioVersion.Biohazard2 || _appContext.SelectedVersion == BioVersion.BiohazardCv;
        if (IsSecondPlayerAvailable)
        {
            Player1Informations.Add(GetRandomPlayerInformation(_appContext.SelectedVersion));
            Player1Informations.AddRange(GetPlayerInformations(_appContext.SelectedRandomizer, Enums.PlayerIndex.Player1));
            SelectedPlayer1Information = Player1Informations.FirstOrDefault(x => string.Equals(x.Name, _appContext.SelectedRandomizer.GetPlayerName((int)Enums.PlayerIndex.Player1), StringComparison.InvariantCultureIgnoreCase));
        }
        else
        {
            IsSwapCharacterEnabled = false;
        }

        var mainCharacters = GetMainPlayersInformation(_appContext.SelectedVersion);
        MainPlayer0Information = mainCharacters.Player0;
        MainPlayer1Information = mainCharacters.Player1;
    }

    public List<PlayerListItemViewModel> GetPlayerInformations(BaseRandomiser baseRandomiser, PlayerIndex playerIndex)
    {
        var playerInformations = new List<PlayerListItemViewModel>();

        var directories = baseRandomiser.GetPlayerCharactersDataDirectories((int)playerIndex);
        foreach (var directory in directories)
        {
            playerInformations.Add(new PlayerListItemViewModel()
            {
                BioVersion = baseRandomiser.BiohazardVersion,
                DirectoryPath = directory,
                FacePngPath = Path.Combine(directory, "face.png"),
                IsFacePngAvailable = File.Exists(Path.Combine(directory, "face.png")),
                Name = Path.GetFileName(directory),
                DisplayName = Path.GetFileName(directory).ToActorString(),
            });
        }

        return playerInformations;
    }

    public (PlayerListItemViewModel Player0, PlayerListItemViewModel Player1) GetMainPlayersInformation(BioVersion bioVersion)
    {
        var player0 = new PlayerListItemViewModel();
        var player1 = new PlayerListItemViewModel();

        switch (bioVersion)
        {
            case BioVersion.Biohazard1:
                player0.BioVersion = BioVersion.Biohazard1;
                player0.DisplayName = "Chris";
                player0.Name = "Chris";
                player0.FacePngPath = "/Resources/Images/RE1/Players/Chris.png";
                player1.BioVersion = BioVersion.Biohazard1;
                player1.DisplayName = "Jill";
                player1.Name = "Jill";
                player1.FacePngPath = "/Resources/Images/RE1/Players/Jill.png";
                break;
            case BioVersion.Biohazard2:
                player0.BioVersion = BioVersion.Biohazard2;
                player0.DisplayName = "Leon";
                player0.Name = "Leon";
                player0.FacePngPath = "/Resources/Images/RE2/Players/Leon.png";
                player1.BioVersion = BioVersion.Biohazard2;
                player1.DisplayName = "Claire";
                player1.Name = "Claire";
                player1.FacePngPath = "/Resources/Images/RE2/Players/Claire.png";
                break;
            case BioVersion.Biohazard3:
                player0.BioVersion = BioVersion.Biohazard3;
                player0.DisplayName = "Jill";
                player0.Name = "Jill";
                player0.FacePngPath = "/Resources/Images/RE3/Players/Jill.png";
                break;
            default:
                throw new InvalidOperationException();
        }

        return (player0, player1);
    }

    private PlayerListItemViewModel GetRandomPlayerInformation(BioVersion bioVersion)
    {
        return new PlayerListItemViewModel()
        {
            FacePngPath = null,
            IsFacePngAvailable = false,
            Name = "Random",
            DisplayName = "Random",
            BioVersion = bioVersion
        };
    }
}
