using HackerNews.MobileApp.Pages.Stories;
using HackerNews.MobileApp.Pages.StoryDetail;
using HackerNews.MobileApp.Services.Browser;
using HackerNews.MobileApp.Services.HackerNews;
using HackerNews.MobileApp.Services.Navigation;

namespace HackerNews.MobileApp
{
    public static class Bootstraper
    {
        public static void InitializeApp(bool useMockServices)
        {
            ViewModelLocator.Initialize();

            ViewModelLocator.RegisterViewModel<StoriesView, StoriesViewModel>();
            ViewModelLocator.RegisterViewModel<StoryView, StoryViewModel>();

            ViewModelLocator.RegisterSingleton<NavigationService, INavigationService>();
            ViewModelLocator.Register<BrowserService, IBrowserService>();
            if (useMockServices)
            {
                ViewModelLocator.Register<HackerNewsService, IHackerNewsService>();
            }
            else
            {
                ViewModelLocator.Register<HackerNewsService, IHackerNewsService>();
            }

            ViewModelLocator.Build();
        }
    }
}
