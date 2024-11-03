using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biorand.desktop.ViewModels.Item;

public class ItemConfigurationViewModel : BindableBase
{
    private ObservableCollection<WeaponItemViewModel> _weaponItems1 = new ObservableCollection<WeaponItemViewModel>();
    private ObservableCollection<WeaponItemViewModel> _weaponItems2 = new ObservableCollection<WeaponItemViewModel>();
    private ObservableCollection<WeaponItemViewModel> _weaponItems3 = new ObservableCollection<WeaponItemViewModel>();

    public ItemConfigurationViewModel()
    {
        WeaponItems1.Add(new WeaponItemViewModel() { DisplayName = "Handgun (Leon)", ImagePath = "/Resources/Images/RE2/Weapons/HandgunLeon.png", IsChecked = true });
        WeaponItems1.Add(new WeaponItemViewModel() { DisplayName = "Shotgun", ImagePath = "/Resources/Images/RE2/Weapons/Shotgun.png", IsChecked = true });
        WeaponItems1.Add(new WeaponItemViewModel() { DisplayName = "Magnum", ImagePath = "/Resources/Images/RE2/Weapons/Magnum.png", IsChecked = true });
        WeaponItems1.Add(new WeaponItemViewModel() { DisplayName = "Colt SAA", ImagePath = "/Resources/Images/RE2/Weapons/ColtSAA.png", IsChecked = true });

        WeaponItems2.Add(new WeaponItemViewModel() { DisplayName = "Handgun (Claire)", ImagePath = "/Resources/Images/RE2/Weapons/HandgunClaire.png", IsChecked = true });
        WeaponItems2.Add(new WeaponItemViewModel() { DisplayName = "Bowgun", ImagePath = "/Resources/Images/RE2/Weapons/Bowgun.png", IsChecked = true });
        WeaponItems2.Add(new WeaponItemViewModel() { DisplayName = "Grenade Launcher", ImagePath = "/Resources/Images/RE2/Weapons/GrenadeLauncher.png", IsChecked = true });
        WeaponItems2.Add(new WeaponItemViewModel() { DisplayName = "Beretta", ImagePath = "/Resources/Images/RE2/Weapons/Beretta.png", IsChecked = true });

        WeaponItems3.Add(new WeaponItemViewModel() { DisplayName = "Rocket Launcher", ImagePath = "/Resources/Images/RE2/Weapons/RocketLauncher.png", IsChecked = true });
        WeaponItems3.Add(new WeaponItemViewModel() { DisplayName = "Flamethrower", ImagePath = "/Resources/Images/RE2/Weapons/Flamethrower.png", IsChecked = true });
        WeaponItems3.Add(new WeaponItemViewModel() { DisplayName = "Sparkshot", ImagePath = "/Resources/Images/RE2/Weapons/Sparkshot.png", IsChecked = true });
        WeaponItems3.Add(new WeaponItemViewModel() { DisplayName = "SMG", ImagePath = "/Resources/Images/RE2/Weapons/SMG.png", IsChecked = true });
    }

    public ObservableCollection<WeaponItemViewModel> WeaponItems1 { get => _weaponItems1; set => SetProperty(ref _weaponItems1, value); }
    public ObservableCollection<WeaponItemViewModel> WeaponItems2 { get => _weaponItems2; set => SetProperty(ref _weaponItems2, value); }
    public ObservableCollection<WeaponItemViewModel> WeaponItems3 { get => _weaponItems3; set => SetProperty(ref _weaponItems3, value); }
}
