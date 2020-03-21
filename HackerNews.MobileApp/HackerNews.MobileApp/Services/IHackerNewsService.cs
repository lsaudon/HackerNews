using System.Collections.Generic;
using System.Threading.Tasks;
using HackerNews.MobileApp.Models;

namespace HackerNews.MobileApp.Services
{
    public interface IHackerNewsService
    {
        Task<IList<int>> NewStories();
        Task<IItem> Item(ItemId itemId);
    }
}
