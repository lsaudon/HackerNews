using System.Threading.Tasks;
using HackerNews.MobileApp.Models;
using HackerNews.MobileApp.Services.HackerNews;
using Xunit;
using NFluent;

namespace HackerNews.MobileApp.Tests.Services
{
    public class HackerNewsServiceTests
    {
        [Fact]
        public async Task WhenNewStoriesThen500Stories()
        {
            IHackerNewsService hackerNewsService = new HackerNewsService();
            var newStories = await hackerNewsService.NewStories();
            Check.That(newStories.Count).IsEqualTo(500);
        }

        [Fact]
        public async Task WhenTopStoriesThen346Stories()
        {
            IHackerNewsService hackerNewsService = new HackerNewsService();
            var topStories = await hackerNewsService.TopStories();
            Check.That(topStories.Count).IsEqualTo(346);
        }

        [Fact]
        public async Task WhenBestStoriesThen200Stories()
        {
            IHackerNewsService hackerNewsService = new HackerNewsService();
            var bestStories = await hackerNewsService.BestStories();
            Check.That(bestStories.Count).IsEqualTo(200);
        }

        [Fact]
        public async Task WhenItem8863ThenStory()
        {
            IHackerNewsService hackerNewsService = new HackerNewsService();
            var story = await hackerNewsService.Story(8863);
            Check.That(story.Id).IsEqualTo(8863);
        }

        [Fact]
        public async Task WhenItem192327ThenJob()
        {
            IHackerNewsService hackerNewsService = new HackerNewsService();
            var item = await hackerNewsService.Item(new ItemId(192327));
            Check.That(item).IsInstanceOf<Job>();
            if (item is Job job)
            {
                Check.That(job.Id).IsEqualTo(192327);
            }
        }

        [Fact]
        public async Task WhenItem2921983ThenComment()
        {
            IHackerNewsService hackerNewsService = new HackerNewsService();
            var item = await hackerNewsService.Item(new ItemId(2921983));
            Check.That(item).IsInstanceOf<Comment>();
            if (item is Comment comment)
            {
                Check.That(comment.Id).IsEqualTo(2921983);
            }
        }

        [Fact]
        public async Task WhenItem126809ThenPoll()
        {
            IHackerNewsService hackerNewsService = new HackerNewsService();
            var item = await hackerNewsService.Item(new ItemId(126809));
            Check.That(item).IsInstanceOf<Poll>();
            if (item is Poll poll)
            {
                Check.That(poll.Id).IsEqualTo(126809);
            }
        }

        [Fact]
        public async Task WhenItem160705ThenPollOpt()
        {
            IHackerNewsService hackerNewsService = new HackerNewsService();
            var item = await hackerNewsService.Item(new ItemId(160705));
            if (item is PollOpt pollOpt)
            {
                Check.That(pollOpt.Id).IsEqualTo(160705);
            }
        }
    }
}
