using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HackerNews.MobileApp.Pages.Base
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomNavigationView
    {
        public CustomNavigationView(Page root) : base(root)
        {
        }
    }
}
