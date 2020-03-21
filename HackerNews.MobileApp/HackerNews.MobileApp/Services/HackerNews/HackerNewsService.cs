using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using HackerNews.MobileApp.Models;
using Newtonsoft.Json;

namespace HackerNews.MobileApp.Services.HackerNews
{
    public class HackerNewsService : IHackerNewsService
    {
        private const string HackerNewsUrl = "https://hacker-news.firebaseio.com/v0/";
        private const string TopStoriesUrl = HackerNewsUrl + "topstories.json";
        private const string NewStoriesUrl = HackerNewsUrl + "newstories.json";
        private const string BestStoriesUrl = HackerNewsUrl + "beststories.json";
        private const string ItemUrl = HackerNewsUrl + "item/";

        public async Task<IList<int>> NewStories()
        {
            var httpClient = new HttpClient();
            var responseString = await httpClient.GetStringAsync(NewStoriesUrl);
            var response = JsonConvert.DeserializeObject<IList<int>>(responseString);
            return response;
        }

        public async Task<IList<int>> TopStories()
        {
            var httpClient = new HttpClient();
            var responseString = await httpClient.GetStringAsync(TopStoriesUrl);
            var response = JsonConvert.DeserializeObject<IList<int>>(responseString);
            return response;
        }

        public async Task<IList<int>> BestStories()
        {
            var httpClient = new HttpClient();
            var responseString = await httpClient.GetStringAsync(BestStoriesUrl);
            var response = JsonConvert.DeserializeObject<IList<int>>(responseString);
            return response;
        }

        public async Task<IItem> Item(ItemId itemId)
        {
            var httpClient = new HttpClient();
            var url = $"{ItemUrl}{itemId.Value}.json";
            var responseString = await httpClient.GetStringAsync(url);
            var item = JsonConvert.DeserializeObject<HackerNewsItemType>(responseString);
            switch (item.Type)
            {
                case ItemType.Job:
                    return JsonConvert.DeserializeObject<Job>(responseString);
                case ItemType.Story:
                    return JsonConvert.DeserializeObject<Story>(responseString);
                case ItemType.Comment:
                    return JsonConvert.DeserializeObject<Comment>(responseString);
                case ItemType.Poll:
                    return JsonConvert.DeserializeObject<Poll>(responseString);
                case ItemType.PollOpt:
                    return JsonConvert.DeserializeObject<PollOpt>(responseString);
                case ItemType.None:
                    throw new ArgumentOutOfRangeException();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public async Task<Comment> Comment(long id)
        {
            var httpClient = new HttpClient();
            var url = $"{ItemUrl}{id}.json";
            var responseString = await httpClient.GetStringAsync(url);
            var item = JsonConvert.DeserializeObject<Comment>(responseString);
            return item;
        }

        public async Task<Story> Story(long id)
        {
            var httpClient = new HttpClient();
            var url = $"{ItemUrl}{id}.json";
            var responseString = await httpClient.GetStringAsync(url);
            var item = JsonConvert.DeserializeObject<Story>(responseString);
            return item;
        }
    }
}
