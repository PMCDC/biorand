using IntelOrca.Biohazard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biorand.desktop.ViewModels.Game;

public class GameListItemViewModel : BindableBase
{
    private BioVersion _bioVersion;
    private string? _displayName;
    private string? _imagePath;
    private bool _isSelected;

    public GameListItemViewModel()
    {

    }

    public BioVersion BioVersion { get => _bioVersion; set => SetProperty(ref _bioVersion, value); }
    public string? DisplayName { get => _displayName; set => SetProperty(ref _displayName, value); }
    public string? ImagePath { get => _imagePath; set => SetProperty(ref _imagePath, value); }
    public bool IsSelected { get => _isSelected; set => SetProperty(ref _isSelected, value); }
}
