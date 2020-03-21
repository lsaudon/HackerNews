using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using HackerNews.MobileApp.Pages.Base;
using HackerNews.MobileApp.Pages.Stories;
using HackerNews.MobileApp.Pages.StoryDetail;
using HackerNews.MobileApp.Services.Browser;
using HackerNews.MobileApp.Services.HackerNews;
using HackerNews.MobileApp.Services.Navigation;
using Xamarin.Forms;

namespace HackerNews.MobileApp
{
    public class ViewModelLocator
    {
        private static IContainer _container;

        private static readonly Dictionary<Type, Type> LinkedViewAndViewModel = new Dictionary<Type, Type>();

        public static readonly BindableProperty AutoWireViewModelProperty =
            BindableProperty.CreateAttached("AutoWireViewModel", typeof(bool), typeof(ViewModelLocator), default(bool),
                propertyChanged: OnAutoWireViewModelChanged);

        public static bool GetAutoWireViewModel(BindableObject bindableObject) =>
            (bool)bindableObject.GetValue(AutoWireViewModelProperty);

        public static void SetAutoWireViewModel(BindableObject bindableObject, bool value) =>
            bindableObject.SetValue(AutoWireViewModelProperty, value);

        public static void RegisterDependencies(bool useMockSerivces)
        {
            var builder = new ContainerBuilder();

            //View models
            AddLinkViewAndViewModel<StoriesView, StoriesViewModel>(builder);
            AddLinkViewAndViewModel<StoryView, StoryViewModel>(builder);

            //Services
            builder.RegisterType<NavigationService>().As<INavigationService>().SingleInstance();
            builder.RegisterType<BrowserService>().As<IBrowserService>();

            if (useMockSerivces)
            {
                builder.RegisterType<HackerNewsService>().As<IHackerNewsService>();
            }
            else
            {
                builder.RegisterType<HackerNewsService>().As<IHackerNewsService>();
            }

            _container?.Dispose();
            _container = builder.Build();
        }

        private static void AddLinkViewAndViewModel<TView, TViewModel>(ContainerBuilder builder)
            where TView : ContentPage
            where TViewModel : ViewModelBase
        {
            builder.RegisterType<TViewModel>();
            LinkedViewAndViewModel.Add(typeof(TView), typeof(TViewModel));
        }

        public static Type FindViewByViewModel(Type viewModelType)
        {
            return LinkedViewAndViewModel.FirstOrDefault(x => x.Value == viewModelType).Key;
        }

        public static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        private static void OnAutoWireViewModelChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = bindable as Element;

            var viewType = view?.GetType();

            if (viewType == null) return;

            var viewModelType = LinkedViewAndViewModel[viewType];

            if (viewModelType == null) return;

            var viewModel = _container.Resolve(viewModelType);
            view.BindingContext = viewModel;
        }
    }
}
