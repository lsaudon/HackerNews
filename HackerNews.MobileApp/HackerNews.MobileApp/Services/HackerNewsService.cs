using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using HackerNews.MobileApp.Models;
using Newtonsoft.Json;

namespace HackerNews.MobileApp.Services
{
    public class HackerNewsService : IHackerNewsService
    {
        private const string HackerNewsUrl = "https://hacker-news.firebaseio.com/v0/";
        private const string TopStoriesUrl = HackerNewsUrl + "newstories.json";
        private const string ItemUrl = HackerNewsUrl + "item/";

        public async Task<IList<int>> NewStories()
        {
            var httpClient = new HttpClient();
            var responseString = await httpClient.GetStringAsync(TopStoriesUrl);
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
    }


}
