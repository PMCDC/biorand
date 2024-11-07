using biorand.app.Attributes.DependencyInjectionAttributes;
using biorand.app.Extensions;
using biorand.app.ViewModels.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace biorand.app.Services
{
    [RegisterSingleton]
    public interface IConfigurationService
    {
        /// <summary>
        /// The BioRand app settings.
        /// </summary>
        BiorandAppSettings BiorandAppSettings { get; }

        /// <summary>
        /// Load the settings from the json file located in the APPDATA folder.
        /// </summary>
        void LoadSettings();

        /// <summary>
        /// Save the current settings into a json file in the biorand appdata folder.
        /// </summary>
        void SaveSettings();
    }

    public class ConfigurationService : IConfigurationService
    {
        private BiorandAppSettings _biorandAppSettings = new BiorandAppSettings();

        public ConfigurationService()
        {
            LoadSettings();
        }

        public BiorandAppSettings BiorandAppSettings { get => _biorandAppSettings; private set { _biorandAppSettings = value; } }

        public void LoadSettings()
        {
            if (File.Exists(BiorandAppSettings.ConfigurationFilePath))
            {
                var settings = JsonSerializer.Deserialize<BiorandAppSettings>(BiorandAppSettings.ConfigurationFilePath);
                BiorandAppSettings = settings != null ? settings : BiorandAppSettings;
            }
            else
            {
                LoadFromOldSettingsFile();
            }
        }

        public void SaveSettings()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Load settings from the old settings file structure.
        /// </summary>
        private void LoadFromOldSettingsFile()
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "biorand", "settings.json");

            if (!File.Exists(path))
                return;

            var oldSettings = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(File.ReadAllText(path));
            if (oldSettings == null)
                return;

            //general
            BiorandAppSettings.LastVersion = oldSettings.GetStringValue("lastVersion", "0.0.0");
            BiorandAppSettings.IsRandomizeTitleVoiceEnabled = oldSettings.GetBoolValue("randomizeTitleVoice", true);

            //re1
            BiorandAppSettings.ResidentEvil1Settings.Seed = oldSettings.GetStringValue("seed1", null);
            BiorandAppSettings.ResidentEvil1Settings.IsEnabled = oldSettings.GetBoolValue("gameEnabled1", false);
            BiorandAppSettings.ResidentEvil1Settings.GameFolderPath = oldSettings.GetStringValue("gamePath1", null);
            BiorandAppSettings.ResidentEvil1Settings.GameExecutableFileName = oldSettings.GetStringValue("gameExecutable1", null);
            BiorandAppSettings.ResidentEvil1Settings.IsIncreaseMaxInventorySizeEnabled = oldSettings.GetBoolValue("maxInventorySize", true);

            //re2
            BiorandAppSettings.ResidentEvil2Settings.Seed = oldSettings.GetStringValue("seed2", null);
            BiorandAppSettings.ResidentEvil2Settings.IsEnabled = oldSettings.GetBoolValue("gameEnabled2", false);
            BiorandAppSettings.ResidentEvil2Settings.GameFolderPath = oldSettings.GetStringValue("gamePath2", null);
            BiorandAppSettings.ResidentEvil2Settings.GameExecutableFileName = oldSettings.GetStringValue("gameExecutable2", null);

            //re3
            BiorandAppSettings.ResidentEvil3Settings.Seed = oldSettings.GetStringValue("seed3", null);
            BiorandAppSettings.ResidentEvil3Settings.IsEnabled = oldSettings.GetBoolValue("gameEnabled3", false);
            BiorandAppSettings.ResidentEvil3Settings.GameFolderPath = oldSettings.GetStringValue("gamePath3", null);
            BiorandAppSettings.ResidentEvil3Settings.GameExecutableFileName = oldSettings.GetStringValue("gameExecutable3", null);

            //re cvx
            BiorandAppSettings.ResidentEvilCodeVeronicaSettings.Seed = oldSettings.GetStringValue("seedCv", null);
            BiorandAppSettings.ResidentEvilCodeVeronicaSettings.IsEnabled = oldSettings.GetBoolValue("gameEnabledCv", false);
            BiorandAppSettings.ResidentEvilCodeVeronicaSettings.GameFolderPath = oldSettings.GetStringValue("gamePathCv", null);
            if (!string.IsNullOrEmpty(BiorandAppSettings.ResidentEvilCodeVeronicaSettings.GameFolderPath))
            {
                BiorandAppSettings.ResidentEvilCodeVeronicaSettings.GameExecutableFileName = Path.GetFileName(BiorandAppSettings.ResidentEvilCodeVeronicaSettings.GameFolderPath);
                BiorandAppSettings.ResidentEvilCodeVeronicaSettings.GameFolderPath = Path.GetDirectoryName(BiorandAppSettings.ResidentEvilCodeVeronicaSettings.GameFolderPath);
            }
            BiorandAppSettings.ResidentEvilCodeVeronicaSettings.IsDoorSkipEnabled = oldSettings.GetBoolValue("doorSkip", true);
        }
    }

}