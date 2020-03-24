using HackerNews.MobileApp.Common;
using HackerNews.MobileApp.Navigations;
using HackerNews.MobileApp.Pages;
using HackerNews.MobileApp.Pages.Stories;
using HackerNews.MobileApp.Pages.StoryDetail;
using HackerNews.MobileApp.Services.Browser;
using HackerNews.MobileApp.Services.HackerNews;

namespace HackerNews.MobileApp
{
    public static class Bootstraper
    {
        public static void InitializeApp(bool useMockServices)
        {
            ViewModelLocator.Initialize();

            ViewModelLocator.Register<ApplicationProvider,IApplicationProvider>();

            ViewModelLocator.RegisterViewModel<StoriesView, StoriesViewModel>();
            ViewModelLocator.RegisterViewModel<StoryView, StoryViewModel>();
            ViewModelLocator.RegisterViewModel<OneView, OneViewModel>();
            ViewModelLocator.RegisterViewModel<TwoView, TwoViewModel>();
            ViewModelLocator.RegisterViewModel<ThreeView, ThreeViewModel>();

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
