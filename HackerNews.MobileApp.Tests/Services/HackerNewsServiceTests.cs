using System.Threading.Tasks;
using HackerNews.MobileApp.Services;
using Xunit;
using NFluent;

namespace HackerNews.MobileApp.Tests.Services
{
    public class HackerNewsServiceTests
    {
        [Fact]
        public async Task WhenNewStoriesThen500Stories()
        {
            var hackerNewsService = new HackerNewsService();
            var newStories = await hackerNewsService.NewStories();
            Check.That(newStories.Count).IsEqualTo(500);
        }

        [Fact]
        public async Task WhenItemThenItem()
        {
            var hackerNewsService = new HackerNewsService();
            var item = await hackerNewsService.Item(8863);
            Check.That(item.Id).IsEqualTo(8863);
        }
    }
}
