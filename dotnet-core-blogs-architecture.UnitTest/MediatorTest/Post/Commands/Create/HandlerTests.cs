using AutoFixture;
using dotnet_core_blogs_architecture.blogs.Mediator.Post.Commands.Create;
using dotnet_core_blogs_architecture.blogs.Mediator.Post.Shared;
using dotnet_core_blogs_architecture.Data.Results;
using dotnet_core_blogs_architecture.infrastructure.Data;
using dotnet_core_blogs_architecture.UnitTest.Helpers;
using Microsoft.Extensions.Logging;
using Moq;

namespace dotnet_core_blogs_architecture.UnitTest.MediatorTest.Post.Commands.Create;
public class HandlerTests
{
    private Handler handler;
    private Mock<IRepository<Data.Models.Post>> mockPostRepository;
    private Mock<ILogger<Handler>> mockLogger;
    private CancellationToken cancellation = CancellationToken.None;

    public HandlerTests()
    {
        mockPostRepository = new Mock<IRepository<Data.Models.Post>>();
        mockLogger = new Mock<ILogger<dotnet_core_blogs_architecture.blogs.Mediator.Post.Commands.Create.Handler>>();
        handler = new Handler(MapperHelper.Instance, mockPostRepository.Object, mockLogger.Object);
    }

    [Fact]
    public async Task ShouldPassHandler()
    {
        var command = AutoFixtureHelper.Fixture.Build<CommandModel>()
            .With(d => d.Title, "Testing Post")
            .With(d => d.Content, "Testing Post Content")
            .Create();
        var PostDbData = AutoFixtureHelper.Fixture.Build<Data.Models.Post>()
            .With(d => d.Title, "Testing Post")
            .With(d => d.Content, "Testing Post Content")
            .Create();
        mockPostRepository.Setup(x => x.AddAsync(It.IsAny<Data.Models.Post>(), cancellation)).ReturnsAsync(PostDbData);
        mockPostRepository.Setup(x => x.SaveChangesAsync(cancellation)).ReturnsAsync(1);
        var result = await handler.Handle(command, cancellation);
        mockPostRepository.Verify(x => x.AddAsync(It.IsAny<Data.Models.Post>(), cancellation), Times.Once);
        mockPostRepository.Verify(x => x.SaveChangesAsync(cancellation), Times.Once);

        Assert.IsType<CreatedResult>(result);
        Assert.IsType<PostResponseModel>((result as CreatedResult).Data);

        var result1 = (result as CreatedResult).Data as PostResponseModel;
        Assert.NotNull(result1);
        Assert.Equal(PostDbData.Title, result1.Title);
        Assert.Equal(PostDbData.Content, result1.Content);
    }

    [Fact]
    public async Task ShouldPassHandlerForNoDataSave()
    {
        var command = AutoFixtureHelper.Fixture.Build<CommandModel>()
            .With(d => d.Title, "Testing Post")
            .With(d => d.Content, "Testing Post Content")
            .Create();
        var PostDbData = AutoFixtureHelper.Fixture.Build<Data.Models.Post>()
            .With(d => d.Title, "Testing Post")
            .With(d => d.Content, "Testing Post Content")
            .Create();

        mockPostRepository.Setup(x => x.AddAsync(It.IsAny<Data.Models.Post>(), cancellation)).ReturnsAsync(PostDbData);
        var result = await handler.Handle(command, cancellation);

        mockPostRepository.Verify(x => x.AddAsync(It.IsAny<Data.Models.Post>(), cancellation), Times.Once);
        mockPostRepository.Verify(x => x.SaveChangesAsync(cancellation), Times.Once);

        Assert.IsType<InvalidValidationResult>(result);
        var errors = (result as InvalidValidationResult).Errors;
        Assert.Equal(1, errors.Count);
        Assert.Equal("Summary", errors[0].Field);
        Assert.Single(errors[0].Description);
        Assert.Equal("There was a server error saving 'Charge Code Data'", errors[0].Description[0]);
    }
}
