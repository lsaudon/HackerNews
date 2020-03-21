using System.Collections.Generic;
using System.Threading.Tasks;
using HackerNews.MobileApp.Models;

namespace HackerNews.MobileApp.Services.HackerNews
{
    public interface IHackerNewsService
    {
        Task<IList<int>> NewStories();
        Task<IList<int>> TopStories();
        Task<IList<int>> BestStories();
        Task<IItem> Item(ItemId itemId);
        Task<Story> Story(long id);
        Task<Comment> Comment(long id);
    }
}
