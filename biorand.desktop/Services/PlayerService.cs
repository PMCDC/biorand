/*using biorand.desktop.Attributes.DependencyInjectionAttributes;
using biorand.desktop.Enums;
using biorand.desktop.ViewModels.Player;
using IntelOrca.Biohazard;
using IntelOrca.Biohazard.BioRand;
using System;
using System.IO;

namespace biorand.desktop.Services;

[RegisterSingleton]
public interface IPlayerService
{
    /// <summary>
    /// Return a collection of the available characters.
    /// </summary>
    /// <param name="baseRandomiser"></param>
    /// <param name="playerIndex"></param>
    /// <returns></returns>
    List<PlayerListItemViewModel> GetPlayerInformations(BaseRandomiser baseRandomiser, PlayerIndex playerIndex);

    /// <summary>
    /// Return the main protagonist information of a given BioHazard version.
    /// </summary>
    /// <param name="bioVersion"></param>
    /// <returns></returns>
    (PlayerListItemViewModel Player0, PlayerListItemViewModel Player1) GetMainPlayersInformation(BioVersion bioVersion);
}

public class PlayerService : IPlayerService
{
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
}*/
