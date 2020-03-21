using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using HackerNews.MobileApp.Models;
using HackerNews.MobileApp.Pages.Base;
using HackerNews.MobileApp.Services.HackerNews;
using MvvmHelpers.Commands;
using Xamarin.Forms;

namespace HackerNews.MobileApp.Pages.StoryDetail
{
    public class StoryViewModel : ViewModelBase
    {
        private readonly IHackerNewsService HackerNewsService;

        public StoryViewModel(IHackerNewsService hackerNewsService)
        {
            HackerNewsService = hackerNewsService;
            Title = "Story";
            LoadStoryCommand = new AsyncCommand<long>(LoadStoryExecute);
        }

        public override Task InitializeAsync(object navigationData)
        {
            var storyId = (long)navigationData;
            LoadStoryCommand.Execute(storyId);
            return base.InitializeAsync(navigationData);
        }

        private Story story;
        public Story Story
        {
            get => story;
            set => SetProperty(ref story, value);
        }

        public ICommand LoadStoryCommand { get; }
        private async Task LoadStoryExecute(long storyId)
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;
                Story = await HackerNewsService.Story(storyId);
            }
            catch (Exception ex)
            {
#if DEBUG
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
#endif
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
