using System;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace HackerNews.MobileApp.Services.Browser
{
    public class BrowserService : IBrowserService
    {
        public async Task<bool> OpenBrowser(Uri uri)
        {
            return await Xamarin.Essentials.Browser.OpenAsync(uri,
                new BrowserLaunchOptions
                {
                    LaunchMode = BrowserLaunchMode.SystemPreferred, TitleMode = BrowserTitleMode.Default
                });
        }
    }
}
