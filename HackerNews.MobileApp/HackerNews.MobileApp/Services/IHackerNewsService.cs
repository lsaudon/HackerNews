using System.Collections.Generic;
using System.Threading.Tasks;

namespace HackerNews.MobileApp.Services
{
    public interface IHackerNewsService
    {
        Task<IList<int>> NewStories();
        Task<Item> Item(int itemId);
    }
}
