using System.Threading.Tasks;
using HackerNews.MobileApp.Pages.Base;

namespace HackerNews.MobileApp.Navigations
{
    public interface INavigationService
    {
        Task InitializeAsync();
        Task PushToAsync<TViewModel>() where TViewModel : ViewModelBase;
        Task PushToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase;

        Task PopAsync();
        Task PopUntilAsync<TViewModel>() where TViewModel : ViewModelBase;
        Task PopUntilAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase;
    }
}
