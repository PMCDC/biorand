using IntelOrca.Biohazard;
using Prism.Mvvm;
using System.IO;

namespace biorand.app.ViewModels.Player
{

    public class PlayerListItemViewModel : BindableBase
    {
        private string _directoryPath;
        private string _facePngPath;
        private string _displayName = string.Empty;
        private string _name = string.Empty;
        private bool _isFacePngAvailable;
        private BioVersion _bioVersion;

        public PlayerListItemViewModel()
        {
        }

        public string DirectoryPath { get => _directoryPath; set => SetProperty(ref _directoryPath, value); }
        public string FacePngPath { get => _facePngPath; set => SetProperty(ref _facePngPath, value); }
        public string DisplayName { get => _displayName; set => SetProperty(ref _displayName, value); }
        public string Name { get => _name; set => SetProperty(ref _name, value); }
        public bool IsFacePngAvailable { get => _isFacePngAvailable; set => SetProperty(ref _isFacePngAvailable, value); }
        public BioVersion BioVersion { get => _bioVersion; set => SetProperty(ref _bioVersion, value); }
    }

}
