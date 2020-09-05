using Listomator.Core;
using Listomator.Views;
using Xamarin.Forms;

namespace Listomator
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var navigationPage = new NavigationPage(new ListomatorShell());
            Locator.NavigationService.Initialize(navigationPage);
            MainPage = navigationPage;
        }

        private static Locator _locator;
        public static Locator Locator => _locator ?? (_locator = new Locator());

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
