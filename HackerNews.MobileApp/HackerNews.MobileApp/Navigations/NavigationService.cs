using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HackerNews.MobileApp.Common;
using HackerNews.MobileApp.Pages.Base;
using HackerNews.MobileApp.Pages.Stories;
using Xamarin.Forms;

namespace HackerNews.MobileApp.Navigations
{
    public class NavigationService : INavigationService
    {
        private readonly IApplicationProvider _applicationProvider;

        private Page MainPage { get => _applicationProvider.MainPage; set => _applicationProvider.MainPage = value; }

        private INavigation Navigation { get => _applicationProvider.MainPage.Navigation; }

        private IReadOnlyList<Page> NavigationStack { get => _applicationProvider.MainPage.Navigation.NavigationStack; }

        public NavigationService(IApplicationProvider applicationProvider)
        {
            _applicationProvider = applicationProvider;
        }

        public Task InitializeAsync() => PushToAsync<StoriesViewModel>();

        public Task PushToAsync<TViewModel>() where TViewModel : ViewModelBase =>
            PushToAsync<TViewModel>(null);

        public Task PushToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase =>
            InternalPushToAsync(typeof(TViewModel), parameter);

        private async Task InternalPushToAsync(Type viewModelType, object parameter)
        {
            if (viewModelType == null)
            {
                throw new ArgumentNullException(nameof(viewModelType));
            }

            var page = CreatePage(viewModelType);

            if (MainPage is CustomNavigationView navigationPage)
            {
                await navigationPage.PushAsync(page);
            }
            else
            {
                MainPage = new CustomNavigationView(page);
            }

            await ((ViewModelBase)page.BindingContext).InitializeAsync(parameter);
        }

        public Task PopAsync() => InternalPopAsync();

        private async Task InternalPopAsync()
        {
            await Navigation.PopAsync();
        }

        public Task PopUntilAsync<TViewModel>() where TViewModel : ViewModelBase => PopUntilAsync<TViewModel>(null);

        public Task PopUntilAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase => InternalPopUntilAsync(typeof(TViewModel), parameter);

        private async Task InternalPopUntilAsync(Type viewModelType, object parameter)
        {
            if (viewModelType == null)
            {
                throw new ArgumentNullException(nameof(viewModelType));
            }

            var isStackWithOnePage = NavigationStack.Count <= 1;
            if (isStackWithOnePage) return;

            var pageType = GetPageTypeForViewModel(viewModelType);

            var page = NavigationStack.FirstOrDefault(x => x.GetType() == pageType);
            if (page == null) return;

            var index = Array.FindIndex(NavigationStack.ToArray(), x => x.GetType() == pageType);
            if (NavigationStack.Count >= 3)
            {
                for (int i = NavigationStack.Count - 2; i > index; i--)
                {
                    Navigation.RemovePage(NavigationStack[i]);
                }
                Navigation.RemovePage(NavigationStack[NavigationStack.Count - 1]);
            }
            else
            {
                Navigation.RemovePage(NavigationStack[index + 1]);
            }

            await ((ViewModelBase)page.BindingContext).InitializeAsync(parameter);
        }

        private Page CreatePage(Type viewModelType)
        {
            if (viewModelType == null)
            {
                throw new ArgumentNullException(nameof(viewModelType));
            }

            var pageType = GetPageTypeForViewModel(viewModelType);

            if (pageType == null)
            {
                throw new ArgumentNullException($"Cannot locate page type for {viewModelType}");
            }

            var page = Activator.CreateInstance(pageType) as Page;
            return page;
        }

        private Type GetPageTypeForViewModel(Type viewModelType)
        {
            if (viewModelType == null)
            {
                throw new ArgumentNullException(nameof(viewModelType));
            }

            var viewType = ViewModelLocator.FindViewByViewModel(viewModelType);
            return viewType;
        }

    }
}
