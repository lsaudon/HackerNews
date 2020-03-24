using System.Windows.Input;
using HackerNews.MobileApp.Navigation;
using HackerNews.MobileApp.Pages.Base;
using MvvmHelpers.Commands;

namespace HackerNews.MobileApp.Pages
{
    public class ThreeViewModel : ViewModelBase
    {
        public ThreeViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = nameof(ThreeViewModel);
        }

        public ICommand GoToOneViewCommand => new AsyncCommand(NavigationService.PushToAsync<OneViewModel>);
        public ICommand GoToTwoViewCommand => new AsyncCommand(NavigationService.PushToAsync<TwoViewModel>);
        public ICommand GoToThreeViewCommand => new AsyncCommand(NavigationService.PushToAsync<ThreeViewModel>);
        public ICommand GoBackViewCommand => new AsyncCommand(NavigationService.PopAsync);
        public ICommand GoUntilOneViewModelViewCommand => new AsyncCommand(() => NavigationService.PopUntilAsync<OneViewModel>("ça va"));
    }
}
