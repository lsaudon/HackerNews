using System.Threading.Tasks;
using HackerNews.MobileApp.Services.Navigation;

namespace HackerNews.MobileApp
{
    public partial class App
    {
        public App()
        {
            InitializeComponent();

            InitializeApp();
        }

        protected override async void OnStart()
        {
            base.OnStart();
            await InitializeNavigation();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        private void InitializeApp()
        {
            ViewModelLocator.RegisterDependencies(true);
        }

        private Task InitializeNavigation()
        {
            var navigationService = ViewModelLocator.Resolve<INavigationService>();
            return navigationService.InitializeAsync();
        }
    }
}
