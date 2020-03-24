using System.Collections.Generic;
using System.Threading.Tasks;
using HackerNews.MobileApp.Models;
using HackerNews.MobileApp.Navigations;
using HackerNews.MobileApp.Pages.Stories;
using HackerNews.MobileApp.Pages.StoryDetail;
using HackerNews.MobileApp.Services.HackerNews;
using Moq;
using NFluent;
using Xunit;

namespace HackerNews.MobileApp.Tests.Pages.Stories
{
    public class StoriesViewModelTests
    {
        private readonly Mock<INavigationService> navigationServiceMock;
        private readonly Mock<IHackerNewsService> hackerNewsServiceMock;

        public StoriesViewModelTests()
        {
            navigationServiceMock = new Mock<INavigationService>();
            hackerNewsServiceMock = new Mock<IHackerNewsService>();
        }

        [Fact]
        public void When_InitializeAsync_Then_LoadNewStoriesExecute()
        {
            IList<int> storyIds = new List<int> { 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
            hackerNewsServiceMock
                .Setup(x => x.NewStories())
                .Returns(Task.FromResult(storyIds));

            hackerNewsServiceMock
                .Setup(x => x.Story(It.IsAny<long>()))
                .Returns(Task.FromResult(new Story { Id = It.IsAny<long>() }));

            var storyViewModel = new StoriesViewModel(navigationServiceMock.Object, hackerNewsServiceMock.Object);
            storyViewModel.InitializeAsync(null);

            Check.That(storyViewModel.Stories).IsNotNull();
            Check.That(storyViewModel.Stories.Count).IsEqualTo(10);
            hackerNewsServiceMock.Verify(x => x.NewStories(), Times.Once());
            hackerNewsServiceMock.Verify(x => x.Story(It.IsAny<long>()), Times.Exactly(10));
        }

        [Fact]
        public void When_LoadNewStoriesCommand_Then_LoadNewStoriesExecute()
        {
            IList<int> storyIds = new List<int> { 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
            hackerNewsServiceMock
                .Setup(x => x.NewStories())
                .Returns(Task.FromResult(storyIds));

            hackerNewsServiceMock
                .Setup(x => x.Story(It.IsAny<long>()))
                .Returns(Task.FromResult(new Story { Id = It.IsAny<long>() }));

            var storyViewModel = new StoriesViewModel(navigationServiceMock.Object, hackerNewsServiceMock.Object);
            storyViewModel.LoadStoriesCommand.Execute(null);

            Check.That(storyViewModel.Stories).IsNotNull();
            Check.That(storyViewModel.Stories.Count).IsEqualTo(10);
            hackerNewsServiceMock.Verify(x => x.NewStories(), Times.Once());
            hackerNewsServiceMock.Verify(x => x.Story(It.IsAny<long>()), Times.Exactly(10));
        }

        [Fact]
        public void When_GoToStoryCommand_Then_GoToStoryExecute()
        {
            var storyViewModel = new StoriesViewModel(navigationServiceMock.Object, hackerNewsServiceMock.Object);

            storyViewModel.GoToStoryCommand.Execute( new Story {Id = 1});

            navigationServiceMock.Verify(x => x.PushToAsync<StoryViewModel>((long)1), Times.Once());
        }
    }
}
