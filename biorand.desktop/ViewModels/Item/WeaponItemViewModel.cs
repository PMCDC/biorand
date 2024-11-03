using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biorand.desktop.ViewModels.Item;

public class WeaponItemViewModel : BindableBase
{
    private string? _displayName;
    private string? _imagePath;
    private bool _isChecked;

    public WeaponItemViewModel()
    {
        CheckWeaponCommand = new DelegateCommand(OnCheckWeaponCommand);
        UncheckWeaponCommand = new DelegateCommand(OnUncheckWeaponCommand);
    }

    public string? DisplayName { get => _displayName; set => SetProperty(ref _displayName, value); }
    public string? ImagePath { get => _imagePath; set => SetProperty(ref _imagePath, value); }
    public bool IsChecked { get => _isChecked; set => SetProperty(ref _isChecked, value); }
    public DelegateCommand UncheckWeaponCommand { get; }
    public DelegateCommand CheckWeaponCommand { get; }

    private void OnCheckWeaponCommand()
    {
        IsChecked = true;
    }

    private void OnUncheckWeaponCommand()
    {
        IsChecked = false;
    }
}
