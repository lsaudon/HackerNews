using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using HackerNews.MobileApp.Pages.Base;
using Xamarin.Forms;

namespace HackerNews.MobileApp
{
    public static class ViewModelLocator
    {
        private static IContainer _container;
        private static ContainerBuilder _builder;

        private static readonly Dictionary<Type, Type> LinkedViewAndViewModel = new Dictionary<Type, Type>();

        public static readonly BindableProperty AutoWireViewModelProperty =
            BindableProperty.CreateAttached("AutoWireViewModel", typeof(bool), typeof(ViewModelLocator), default(bool),
                propertyChanged: OnAutoWireViewModelChanged);

        public static bool GetAutoWireViewModel(BindableObject bindable)
        {
            return (bool)bindable.GetValue(AutoWireViewModelProperty);
        }

        public static void SetAutoWireViewModel(BindableObject bindable, bool value)
        {
            bindable.SetValue(AutoWireViewModelProperty, value);
        }

        public static void Initialize()
        {
            _builder = new ContainerBuilder();
            LinkedViewAndViewModel.Clear();
        }

        public static void RegisterSingleton<T, TInterface>()
        {
            _builder.RegisterType<T>().As<TInterface>().SingleInstance();
        }

        public static void Register<T, TInterface>()
        {
            _builder.RegisterType<T>().As<TInterface>();
        }

        public static void RegisterViewModel<TView, TViewModel>()
            where TView : ContentPage
            where TViewModel : ViewModelBase
        {
            _builder.RegisterType<TViewModel>();
            LinkedViewAndViewModel.Add(typeof(TView), typeof(TViewModel));
        }

        public static void Build()
        {
            _container?.Dispose();
            _container = _builder.Build();
        }

        public static T Resolve<T>()
            where T : class
        {
            return _container.Resolve<T>();
        }

        public static Type FindViewByViewModel(Type viewModelType)
        {
            return LinkedViewAndViewModel.FirstOrDefault(x => x.Value == viewModelType).Key;
        }

        private static void OnAutoWireViewModelChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = bindable as Element;
            if (view == null)
            {
                return;
            }

            var viewType = view.GetType();

            var viewModelType = LinkedViewAndViewModel[viewType];

            var viewModel = _container.Resolve(viewModelType);
            view.BindingContext = viewModel;
        }

    }
}
