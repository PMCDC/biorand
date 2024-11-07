using biorand.app.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace biorand.app.ViewModels.Settings
{
    public class BiorandAppSettings
    {
        [JsonPropertyName("last_version")]
        public string LastVersion { get; set; } = "0.0.0";

        [JsonPropertyName("randomize_title_voice")]
        public bool IsRandomizeTitleVoiceEnabled { get; set; }

        [JsonPropertyName("resident_evil_1")]
        public ResidentEvil1GameSettings ResidentEvil1Settings { get; set; } = new ResidentEvil1GameSettings();

        [JsonPropertyName("resident_evil_2")]
        public ResidentEvilGameBaseSettings ResidentEvil2Settings { get; set; } = new ResidentEvilGameBaseSettings();

        [JsonPropertyName("resident_evil_3")]
        public ResidentEvilGameBaseSettings ResidentEvil3Settings { get; set; } = new ResidentEvilGameBaseSettings();

        [JsonPropertyName("resident_evil_code_veronica")]
        public ResidentEvilCodeVeronicaSettings ResidentEvilCodeVeronicaSettings { get; set; } = new ResidentEvilCodeVeronicaSettings();

        [JsonIgnore]
        public string ConfigurationFilePath { get => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "biorand", "biorand.settings.json"); }
    }

    public class ResidentEvilGameBaseSettings
    {
        [JsonPropertyName("seed")]
        public string Seed { get; set; }

        [JsonPropertyName("enabled")]
        public bool IsEnabled { get; set; }

        [JsonPropertyName("directory")]
        public string GameFolderPath { get; set; }

        [JsonPropertyName("executable")]
        public string GameExecutableFileName { get; set; }

        [JsonIgnore]
        public string GameExecutableFullPath { get => !string.IsNullOrEmpty(GameFolderPath) && !string.IsNullOrEmpty(GameExecutableFileName) ? Path.Combine(GameFolderPath, GameExecutableFileName) : null; }
    }

    public class ResidentEvil1GameSettings : ResidentEvilGameBaseSettings
    {
        [JsonPropertyName("enable_max_inventory")]
        public bool IsIncreaseMaxInventorySizeEnabled { get; set; }
    }

    public class ResidentEvilCodeVeronicaSettings : ResidentEvilGameBaseSettings
    {
        [JsonPropertyName("enable_door_skip")]
        public bool IsDoorSkipEnabled { get; set; }
    }

}

