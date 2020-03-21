using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using HackerNews.MobileApp.Models;
using HackerNews.MobileApp.Pages.Base;
using HackerNews.MobileApp.Pages.StoryDetail;
using HackerNews.MobileApp.Services.HackerNews;
using MvvmHelpers;
using MvvmHelpers.Commands;
using Xamarin.Forms;

namespace HackerNews.MobileApp.Pages.Stories
{
    public class StoriesViewModel : ViewModelBase
    {
        private readonly IHackerNewsService HackerNewsService;

        public StoriesViewModel(IHackerNewsService hackerNewsService)
        {
            HackerNewsService = hackerNewsService;
            Title = "New Stories";
            LoadStoriesCommand = new AsyncCommand(LoadStoriesExecute);
            GoToStoryCommand = new AsyncCommand<Story>(GoToStoryExecute);
        }

        public override Task InitializeAsync(object navigationData)
        {
            LoadStoriesCommand.Execute(null);
            return base.InitializeAsync(navigationData);
        }

        public ObservableRangeCollection<Story> Items { get; set; } = new ObservableRangeCollection<Story>();

        public ICommand LoadStoriesCommand { get; }
        private async Task LoadStoriesExecute()
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;

                var ids = await HackerNewsService.NewStories();

                var items = new List<Story>();
                foreach (var id in ids.Take(10))
                {
                    var item = await HackerNewsService.Story(id);
                    items.Add(item);
                }

                if (Items.Count > 0)
                    Items.ReplaceRange(items);
                else
                    Items.AddRange(items);
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

        public ICommand GoToStoryCommand { get; }

        private async Task GoToStoryExecute(Story story)
        {
            await NavigationService.NavigateToAsync<StoryViewModel>(story.Id);
        }
    }
}
