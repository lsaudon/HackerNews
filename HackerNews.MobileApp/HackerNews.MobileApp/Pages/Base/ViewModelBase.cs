using System.Threading.Tasks;
using HackerNews.MobileApp.Services.Navigation;
using MvvmHelpers;

namespace HackerNews.MobileApp.Pages.Base
{
    public class ViewModelBase : BaseViewModel
    {
        protected readonly INavigationService NavigationService;

        protected ViewModelBase() => NavigationService = ViewModelLocator.Resolve<INavigationService>();

        public virtual Task InitializeAsync(object navigationData) => Task.FromResult(false);
    }
}
