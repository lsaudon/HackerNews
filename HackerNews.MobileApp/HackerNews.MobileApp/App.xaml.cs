using System.Threading.Tasks;
using HackerNews.MobileApp.Navigations;

namespace HackerNews.MobileApp
{
    public partial class App
    {
        public App()
        {
            InitializeComponent();

            Bootstraper.InitializeApp(true);
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

        private Task InitializeNavigation()
        {
            var navigationService = ViewModelLocator.Resolve<INavigationService>();
            return navigationService.InitializeAsync();
        }
    }
}
