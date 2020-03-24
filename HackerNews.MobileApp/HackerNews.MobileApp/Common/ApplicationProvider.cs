using Xamarin.Forms;

namespace HackerNews.MobileApp.Common
{
    public class ApplicationProvider : IApplicationProvider
    {
        public Page MainPage
        {
            get => Application.Current.MainPage;
            set => Application.Current.MainPage = value;
        }
    }
}
