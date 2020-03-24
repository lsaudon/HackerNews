using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HackerNews.MobileApp.Models;
using HackerNews.MobileApp.Navigations;
using HackerNews.MobileApp.Pages.StoryDetail;
using HackerNews.MobileApp.Services.Browser;
using HackerNews.MobileApp.Services.HackerNews;
using Moq;
using NFluent;
using Xunit;

namespace HackerNews.MobileApp.Tests.Pages.StoryDetail
{
    public class StoryViewModelTests
    {
        private readonly Mock<INavigationService> navigationServiceMock;
        private readonly Mock<IHackerNewsService> hackerNewsServiceMock;
        private readonly Mock<IBrowserService> browserServiceMock;

        public StoryViewModelTests()
        {
            navigationServiceMock = new Mock<INavigationService>();
            hackerNewsServiceMock = new Mock<IHackerNewsService>();
            browserServiceMock = new Mock<IBrowserService>();
        }

        [Fact]
        public void When_InitializeAsync_Then_LoadNewStoriesExecute()
        {
            var storyId = (long)1;
            var commentId = new List<long> { 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
            hackerNewsServiceMock
                .Setup(x => x.Story(storyId))
                .Returns(Task.FromResult(new Story { Id = storyId, Kids = commentId }));

            hackerNewsServiceMock
                .Setup(x => x.Comment(It.IsAny<long>()))
                .Returns(Task.FromResult(new Comment { Id = It.IsAny<long>() }));

            var storyViewModel = new StoryViewModel(navigationServiceMock.Object, hackerNewsServiceMock.Object, browserServiceMock.Object);
            storyViewModel.InitializeAsync(storyId);

            Check.That(storyViewModel.Story).IsNotNull();
            Check.That(storyViewModel.Story.Id).IsEqualTo(1);
            Check.That(storyViewModel.Comments.Count).IsEqualTo(10);
            hackerNewsServiceMock.Verify(x => x.Story(It.IsAny<long>()), Times.Once());
            hackerNewsServiceMock.Verify(x => x.Comment(It.IsAny<long>()), Times.Exactly(10));
        }

        [Fact]
        public void When_LoadNewStoriesCommand_Then_LoadNewStoriesExecute()
        {
            var storyId = (long)1;
            var commentIds = new List<long> { 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };

            hackerNewsServiceMock
                .Setup(x => x.Story(storyId))
                .Returns(Task.FromResult(new Story { Id = storyId, Kids = commentIds }));

            hackerNewsServiceMock
                .Setup(x => x.Comment(It.IsAny<long>()))
                .Returns(Task.FromResult(new Comment { Id = It.IsAny<long>() }));

            var storyViewModel = new StoryViewModel(navigationServiceMock.Object, hackerNewsServiceMock.Object, browserServiceMock.Object);
            storyViewModel.LoadStoryCommand.Execute(storyId);

            Check.That(storyViewModel.Story).IsNotNull();
            Check.That(storyViewModel.Story.Id).IsEqualTo(1);
            Check.That(storyViewModel.Comments.Count).IsEqualTo(10);
            hackerNewsServiceMock.Verify(x => x.Story(It.IsAny<long>()), Times.Once());
            hackerNewsServiceMock.Verify(x => x.Comment(It.IsAny<long>()), Times.Exactly(10));
        }

        [Fact]
        public void When_LoadNewStoriesCommandWithoutComment_Then_LoadNewStoriesExecute()
        {
            var storyId = (long)1;

            hackerNewsServiceMock
                .Setup(x => x.Story(storyId))
                .Returns(Task.FromResult(new Story { Id = storyId, Kids = null }));

            hackerNewsServiceMock
                .Setup(x => x.Comment(It.IsAny<long>()))
                .Returns(Task.FromResult(new Comment { Id = It.IsAny<long>() }));

            var storyViewModel = new StoryViewModel(navigationServiceMock.Object, hackerNewsServiceMock.Object, browserServiceMock.Object);
            storyViewModel.LoadStoryCommand.Execute(storyId);

            Check.That(storyViewModel.Story).IsNotNull();
            Check.That(storyViewModel.Story.Id).IsEqualTo(1);
            Check.That(storyViewModel.Comments.Count).IsEqualTo(0);
            hackerNewsServiceMock.Verify(x => x.Story(It.IsAny<long>()), Times.Once());
        }


        [Fact]
        public void When_GoToUrlCommand_Then_GoToUrlExecute()
        {
            browserServiceMock
                .Setup(x => x.OpenBrowser(It.IsAny<Uri>()))
                .Returns(Task.FromResult(true));

            var storyViewModel = new StoryViewModel(navigationServiceMock.Object, hackerNewsServiceMock.Object, browserServiceMock.Object);
            storyViewModel.GoToUrlCommand.Execute(new Uri("https://www.twitch.tv/"));

            browserServiceMock.Verify(x => x.OpenBrowser(It.IsAny<Uri>()), Times.Once());
        }
    }
}
