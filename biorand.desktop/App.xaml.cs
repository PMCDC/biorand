using biorand.desktop.Views;
using biorand.desktop.Extensions;
using System.Windows;

namespace biorand.desktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return new MainWindow(); 
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterDependencies();
            //throw new NotImplementedException();
        }
    }

}
