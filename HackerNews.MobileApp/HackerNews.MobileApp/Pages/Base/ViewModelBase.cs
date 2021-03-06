﻿using System.Threading.Tasks;
using HackerNews.MobileApp.Navigations;
using MvvmHelpers;

namespace HackerNews.MobileApp.Pages.Base
{
    public class ViewModelBase : BaseViewModel
    {
        protected readonly INavigationService NavigationService;

        protected ViewModelBase(INavigationService navigationService) => NavigationService = navigationService;

        public virtual Task InitializeAsync(object navigationData) => Task.FromResult(false);
    }
}
