using System.Collections.Generic;
using HackerNews.MobileApp.Common;
using HackerNews.MobileApp.Navigation;
using HackerNews.MobileApp.Pages.Base;
using NFluent;
using Xamarin.Forms;
using Xunit;

namespace HackerNews.MobileApp.Tests
{
    public class ViewModelLocatorTests
    {
      [Fact]
        public void ShouldCheckAutoWireViewModelProperty()
        {
            Check.That(ViewModelLocator.AutoWireViewModelProperty.PropertyName).IsEqualTo("AutoWireViewModel");
            Check.That(ViewModelLocator.AutoWireViewModelProperty.ReturnType).IsEqualTo(typeof(bool));
            Check.That(ViewModelLocator.AutoWireViewModelProperty.DeclaringType).IsEqualTo(typeof(ViewModelLocator));
            Check.That(ViewModelLocator.AutoWireViewModelProperty.DefaultValue).IsEqualTo(default(bool));
        }

        [Fact]
        public void WhenOnAutoWireViewModelChangedThenViewBindingContextIsViewModel()
        {
            InitializeAViewModelIoC();

            var view = new AView();

            var autoWireViewModel = ViewModelLocator.GetAutoWireViewModel(view);
            Check.That(autoWireViewModel).IsFalse();

            ViewModelLocator.SetAutoWireViewModel(view,true);

            Check.That(view.BindingContext).IsInstanceOf<AViewModel>();

            autoWireViewModel = ViewModelLocator.GetAutoWireViewModel(view);
            Check.That(autoWireViewModel).IsTrue();
        }

        [Fact]
        public void WhenOnAutoWireViewModelChangedAndViewModelNotRegisterThenNothing()
        {
            var view = new AView();

            Check.ThatCode(() => ViewModelLocator.SetAutoWireViewModel(view, true)).Throws<KeyNotFoundException>();
        }

        [Fact]
        public void WhenFindViewByViewModelThenView()
        {
            InitializeAViewModelIoC();
            var type = ViewModelLocator.FindViewByViewModel(typeof(AViewModel));
            Check.That(type).IsEqualTo(typeof(AView));
        }

        [Fact]
        public void WhenRegisterAndBuildThenReturnObject()
        {
            InitializeAViewModelIoC();

            var aViewModel = ViewModelLocator.Resolve<AViewModel>();
            Check.That(aViewModel.GetType()).IsEqualTo(typeof(AViewModel));
        }

        private static void InitializeAViewModelIoC()
        {
            ViewModelLocator.Initialize();
            ViewModelLocator.Register<ApplicationProvider,IApplicationProvider>();
            ViewModelLocator.RegisterSingleton<NavigationService, INavigationService>();
            ViewModelLocator.RegisterViewModel<AView, AViewModel>();
            ViewModelLocator.Build();
        }

        private class AView : ContentPage { }
        private class AViewModel : ViewModelBase
        {
            public AViewModel(INavigationService navigationService) : base(navigationService)
            {
            }
        }
    }
}
