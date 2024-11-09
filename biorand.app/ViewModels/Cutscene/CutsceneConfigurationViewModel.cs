using IntelOrca.Biohazard;

namespace biorand.app.ViewModels.Cutscene
{


    public class CutsceneConfigurationViewModel : BaseViewModel
    {
        private bool _isCustomEventAvailable;

        public CutsceneConfigurationViewModel(IAppContext appContext) : base(appContext)
        {
            RefreshControls();
        }

        public bool IsCustomEventAvailable { get => _isCustomEventAvailable; set => SetProperty(ref _isCustomEventAvailable, value); }

        public override void RefreshControls()
        {
            switch (_appContext.SelectedVersion)
            {
                case BioVersion.Biohazard1:
                case BioVersion.Biohazard3:
                    IsCustomEventAvailable = false;
                    break;
                case BioVersion.Biohazard2:
                    IsCustomEventAvailable = true;
                    break;
            }

            IsAvailable = _appContext.SelectedVersion != BioVersion.BiohazardCv;
        }
    }
}


