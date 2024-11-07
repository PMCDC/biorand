using biorand.app.Extensions;
using IntelOrca.Biohazard;
using IntelOrca.Biohazard.BioRand;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using static biorand.app.Behaviors.TextBoxBehaviors.TextBoxBehaviors;

namespace biorand.app.ViewModels.Seed
{
    public class SeedConfigurationViewModel : BindableBase
    {
        private readonly IAppContext _appContext;

        private BioVersion _bioVersion;
        private RandoConfig _seed;
        private string _seedText;
        private string _playerName0;
        private string _playerName1;

        public SeedConfigurationViewModel(IAppContext appContext)
        {
            _appContext = appContext;
            _seed = new RandoConfig();
            _seedText = Seed.ToString();
            _bioVersion = appContext.SelectedVersion;
            _playerName0 = appContext.SelectedVersion.GetPlayer0();
            _playerName1 = appContext.SelectedVersion.GetPlayer1();

            SeedTextChangedCommand = new DelegateCommand<TextBoxTextChangedArgs>(OnSeedTextChangedCommand);
        }

        public BioVersion BioVersion { get => _bioVersion; set => SetProperty(ref _bioVersion, value); }
        public RandoConfig Seed { get => _seed; set => SetProperty(ref _seed, value); }
        public string SeedText { get => _seedText; set => SetProperty(ref _seedText, value); }
        public string PlayerName0 { get => _playerName0; set => SetProperty(ref _playerName0, value); }
        public string PlayerName1 { get => _playerName1; set => SetProperty(ref _playerName1, value); }


        public DelegateCommand<TextBoxTextChangedArgs> SeedTextChangedCommand { get; }

        /// <summary>
        /// todo
        /// </summary>
        /// <param name="args"></param>
        private void OnSeedTextChangedCommand(TextBoxTextChangedArgs args)
        {
            /*var txt = args.TextBox.Text;
            foreach (var change in args.TextChangedEventArgs.Changes)
            {
                if (change.AddedLength > 0 && change.RemovedLength > 0)
                {
                }
                else if (change.AddedLength > 0)
                {
                    if (change.Offset + 1 < txt.Length)
                    {
                        txt = txt.Remove(change.Offset + 1, 1);
                    }
                }
                else if (change.RemovedLength > 0)
                {
                    txt = txt.Insert(change.Offset, "0");
                }
            }

            var config = RandoConfig.FromString(txt);
            var caretIndex = args.TextBox.CaretIndex;

            if (config.Game != (SelectedGame + 1))
            {
                if (_config.Game >= 1 && _config.Game <= 4)
                {
                    SelectedGame = _config.Game - 1;
                    _config = RandoConfig.FromString(txt);
                }
                else
                {
                    _config.Game = (byte)(SelectedGame + 1);
                }
            }
            if (_config.Game != 2)
            {
                _config.Scenario = 0;
                if (_config.Game != 1)
                {
                    _config.SwapCharacters = false;
                }
                _config.RandomEvents = false;
            }

            UpdateUi();
            txtSeed.Text = _config.ToString();
            txtSeed.CaretIndex = Math.Min(caretIndex, txtSeed.Text.Length);*/
        }

        public void RefreshControls()
        {
            BioVersion = _appContext.SelectedVersion;
            PlayerName0 = _appContext.SelectedVersion.GetPlayer0();
            PlayerName1 = _appContext.SelectedVersion.GetPlayer1();
        }
    }
}


