using System;
using System.Threading.Tasks;

namespace HackerNews.MobileApp.Services.Browser
{
    public interface IBrowserService
    {
        Task<bool> OpenBrowser(Uri uri);
    }
}
