using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biorand.desktop.ViewModels.Item;

public class ItemConfigurationViewModel : BindableBase
{
    private readonly IAppContext _appContext;
    private ObservableCollection<WeaponItemViewModel> _weaponItems = new ObservableCollection<WeaponItemViewModel>();

    public ItemConfigurationViewModel(IAppContext appContext)
    {
        _appContext = appContext;
        LoadWeapons();
    }

    public ObservableCollection<WeaponItemViewModel> WeaponItems { get => _weaponItems; set => SetProperty(ref _weaponItems, value); }

    public void RefreshControls()
    {
        LoadWeapons();
    }

    private void LoadWeapons()
    {
        WeaponItems.Clear();
        switch (_appContext.SelectedVersion)
        {
            case IntelOrca.Biohazard.BioVersion.Biohazard1:
                WeaponItems.Add(new WeaponItemViewModel() { DisplayName = "Beretta", ImagePath = "/Resources/Images/RE1/Weapons/Beretta.png", IsChecked = true });
                WeaponItems.Add(new WeaponItemViewModel() { DisplayName = "Shotgun", ImagePath = "/Resources/Images/RE1/Weapons/Shotgun.png", IsChecked = true });
                WeaponItems.Add(new WeaponItemViewModel() { DisplayName = "Flame Thrower", ImagePath = "/Resources/Images/RE1/Weapons/FlameThrower.png", IsChecked = true });
                WeaponItems.Add(new WeaponItemViewModel() { DisplayName = "Bazooka", ImagePath = "/Resources/Images/RE1/Weapons/Bazooka.png", IsChecked = true });
                WeaponItems.Add(new WeaponItemViewModel() { DisplayName = "Magnum", ImagePath = "/Resources/Images/RE1/Weapons/Magnum.png", IsChecked = true });
                WeaponItems.Add(new WeaponItemViewModel() { DisplayName = "Rocket Launcher", ImagePath = "/Resources/Images/RE1/Weapons/RocketLauncher.png", IsChecked = true });
                break;
            case IntelOrca.Biohazard.BioVersion.Biohazard2:
                WeaponItems.Add(new WeaponItemViewModel() { DisplayName = "Handgun (Leon)", ImagePath = "/Resources/Images/RE2/Weapons/HandgunLeon.png", IsChecked = true });
                WeaponItems.Add(new WeaponItemViewModel() { DisplayName = "Handgun (Claire)", ImagePath = "/Resources/Images/RE2/Weapons/HandgunClaire.png", IsChecked = true });
                WeaponItems.Add(new WeaponItemViewModel() { DisplayName = "SMG", ImagePath = "/Resources/Images/RE2/Weapons/SMG.png", IsChecked = true });
                WeaponItems.Add(new WeaponItemViewModel() { DisplayName = "Flamethrower", ImagePath = "/Resources/Images/RE2/Weapons/Flamethrower.png", IsChecked = true });
                WeaponItems.Add(new WeaponItemViewModel() { DisplayName = "Colt SAA", ImagePath = "/Resources/Images/RE2/Weapons/ColtSAA.png", IsChecked = true });
                WeaponItems.Add(new WeaponItemViewModel() { DisplayName = "Beretta", ImagePath = "/Resources/Images/RE2/Weapons/Beretta.png", IsChecked = true });
                WeaponItems.Add(new WeaponItemViewModel() { DisplayName = "Sparkshot", ImagePath = "/Resources/Images/RE2/Weapons/Sparkshot.png", IsChecked = true });
                WeaponItems.Add(new WeaponItemViewModel() { DisplayName = "Bowgun", ImagePath = "/Resources/Images/RE2/Weapons/Bowgun.png", IsChecked = true });
                WeaponItems.Add(new WeaponItemViewModel() { DisplayName = "Shotgun", ImagePath = "/Resources/Images/RE2/Weapons/Shotgun.png", IsChecked = true });
                WeaponItems.Add(new WeaponItemViewModel() { DisplayName = "Magnum", ImagePath = "/Resources/Images/RE2/Weapons/Magnum.png", IsChecked = true });
                WeaponItems.Add(new WeaponItemViewModel() { DisplayName = "Rocket Launcher", ImagePath = "/Resources/Images/RE2/Weapons/RocketLauncher.png", IsChecked = true });
                WeaponItems.Add(new WeaponItemViewModel() { DisplayName = "Grenade Launcher", ImagePath = "/Resources/Images/RE2/Weapons/GrenadeLauncher.png", IsChecked = true });
                break;
            case IntelOrca.Biohazard.BioVersion.Biohazard3:
                WeaponItems.Add(new WeaponItemViewModel() { DisplayName = "Handgun (Sigpro)", ImagePath = "/Resources/Images/RE3/Weapons/Sigpro.png", IsChecked = true });
                WeaponItems.Add(new WeaponItemViewModel() { DisplayName = "Handgun (Beretta)", ImagePath = "/Resources/Images/RE3/Weapons/Beretta.png", IsChecked = true });
                WeaponItems.Add(new WeaponItemViewModel() { DisplayName = "Shotgun (Beneli)", ImagePath = "/Resources/Images/RE3/Weapons/ShotgunBeneli.png", IsChecked = true });
                WeaponItems.Add(new WeaponItemViewModel() { DisplayName = "Magnum", ImagePath = "/Resources/Images/RE3/Weapons/Magnum.png", IsChecked = true });
                WeaponItems.Add(new WeaponItemViewModel() { DisplayName = "Grenade Launcher", ImagePath = "/Resources/Images/RE3/Weapons/GrenadeLauncher.png", IsChecked = true });
                WeaponItems.Add(new WeaponItemViewModel() { DisplayName = "Rocket Launcher", ImagePath = "/Resources/Images/RE3/Weapons/RocketLauncher.png", IsChecked = true });
                WeaponItems.Add(new WeaponItemViewModel() { DisplayName = "Mine Thrower", ImagePath = "/Resources/Images/RE3/Weapons/MineThrower.png", IsChecked = true });
                WeaponItems.Add(new WeaponItemViewModel() { DisplayName = "Handgun (Eagle)", ImagePath = "/Resources/Images/RE3/Weapons/Eagle.png", IsChecked = true });
                WeaponItems.Add(new WeaponItemViewModel() { DisplayName = "Assault Riffle", ImagePath = "/Resources/Images/RE3/Weapons/AssaultRiffle.png", IsChecked = true });
                WeaponItems.Add(new WeaponItemViewModel() { DisplayName = "Shotgun (M37)", ImagePath = "/Resources/Images/RE3/Weapons/ShotgunM37.png", IsChecked = true });
                break;
        }
    }
}
