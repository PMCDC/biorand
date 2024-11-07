using biorand.app.Attributes.DependencyInjectionAttributes;
using biorand.app.Services;
using IntelOrca.Biohazard;
using IntelOrca.Biohazard.BioRand;
using IntelOrca.Biohazard.BioRand.RE1;
using IntelOrca.Biohazard.BioRand.RE2;
using IntelOrca.Biohazard.BioRand.RE3;
using IntelOrca.Biohazard.BioRand.RECV;
using System.Collections.Generic;

namespace biorand.app
{
    [RegisterSingleton]
    public interface IAppContext
    {
        /// <summary>
        /// The Randomizer Version currently selected within the Application.
        /// </summary>
        BioVersion SelectedVersion { get; }

        /// <summary>
        /// The Randomizer currently selected within the Application.
        /// </summary>
        BaseRandomiser SelectedRandomizer { get; }

        void SetSelectedVersion(BioVersion bioVersion);
    }

    public class AppContext : IAppContext
    {
        private IConfigurationService _configurationService;

        private BioVersion _selectedVersion;
        private Dictionary<BioVersion, BaseRandomiser> _randomizers = new Dictionary<BioVersion, BaseRandomiser>();

        public AppContext(IConfigurationService configurationService)
        {
            _configurationService = configurationService;
            _selectedVersion = BioVersion.Biohazard2;
            ReloadRandomizers();
        }

        public BioVersion SelectedVersion => _selectedVersion;

        public BaseRandomiser SelectedRandomizer => _randomizers[SelectedVersion];

        public void SetSelectedVersion(BioVersion bioVersion)
        {
            _selectedVersion = bioVersion;
        }

        private void ReloadRandomizers()
        {
            var reInstall = GetReInstallConfig();
            _randomizers[BioVersion.Biohazard1] = new Re1Randomiser(reInstall, null);
            _randomizers[BioVersion.Biohazard2] = new Re2Randomiser(reInstall, null);
            _randomizers[BioVersion.Biohazard3] = new Re3Randomiser(reInstall, null);
            _randomizers[BioVersion.BiohazardCv] = new ReCvRandomiser(reInstall, null);
        }

        /// <summary>
        /// Return the settings object required for the derived class that implements the BaseRandomizer class.
        /// </summary>
        /// <returns></returns>
        private ReInstallConfig GetReInstallConfig()
        {
            var reInstallConfig = new ReInstallConfig();

            //shared
            reInstallConfig.EnableCustomContent = false; //todo
            reInstallConfig.RandomizeTitleVoice = _configurationService.BiorandAppSettings.IsRandomizeTitleVoiceEnabled;

            //re1
            reInstallConfig.SetEnabled(0, _configurationService.BiorandAppSettings.ResidentEvil1Settings.IsEnabled);
            reInstallConfig.SetInstallPath(0, _configurationService.BiorandAppSettings.ResidentEvil1Settings.GameFolderPath);
            reInstallConfig.MaxInventorySize = _configurationService.BiorandAppSettings.ResidentEvil1Settings.IsIncreaseMaxInventorySizeEnabled;

            //re2
            reInstallConfig.SetEnabled(1, _configurationService.BiorandAppSettings.ResidentEvil2Settings.IsEnabled);
            reInstallConfig.SetInstallPath(1, _configurationService.BiorandAppSettings.ResidentEvil2Settings.GameFolderPath);

            //re3
            reInstallConfig.SetEnabled(2, _configurationService.BiorandAppSettings.ResidentEvil3Settings.IsEnabled);
            reInstallConfig.SetInstallPath(2, _configurationService.BiorandAppSettings.ResidentEvil3Settings.GameFolderPath);

            //re cvx
            reInstallConfig.SetEnabled(3, _configurationService.BiorandAppSettings.ResidentEvilCodeVeronicaSettings.IsEnabled);
            reInstallConfig.SetInstallPath(3, _configurationService.BiorandAppSettings.ResidentEvilCodeVeronicaSettings.GameExecutableFullPath);
            reInstallConfig.DoorSkip = _configurationService.BiorandAppSettings.ResidentEvilCodeVeronicaSettings.IsDoorSkipEnabled;

            return reInstallConfig;
        }
    }

}

