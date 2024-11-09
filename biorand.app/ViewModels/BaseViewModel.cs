using Prism.Mvvm;

namespace biorand.app.ViewModels
{
    public abstract class BaseViewModel : BindableBase
    {
        protected readonly IAppContext _appContext;
        private bool _isAvailable = true;

        public BaseViewModel(IAppContext appContext)
        {
            _appContext = appContext;
        }

        public bool IsAvailable { get => _isAvailable; set { SetProperty(ref _isAvailable, value); } }

        public abstract void RefreshControls();
    }
}
