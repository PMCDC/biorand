using biorand.app.Extensions;
using Prism.Ioc;
using Prism.Unity;
using System.Windows;

namespace biorand.app
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return new Views.MainWindow();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterDependencies();
        }
    }
}
