using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
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

        public async Task<Item> Item(int itemId)
        {
            var httpClient = new HttpClient();
            var url = $"{ItemUrl}{itemId}.json";
            var responseString = await httpClient.GetStringAsync(url);
            var response = JsonConvert.DeserializeObject<Item>(responseString);
            return response;
        }
    }

    public class Item
    {
        public int Id { get; set; }
    }
}
