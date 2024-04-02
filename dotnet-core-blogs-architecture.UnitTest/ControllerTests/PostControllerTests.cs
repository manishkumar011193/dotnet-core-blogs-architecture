using AutoFixture;
using dotnet_core_blogs_architecture.blogs.Controllers;
using dotnet_core_blogs_architecture.Data.Results;
using dotnet_core_blogs_architecture.UnitTest.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace DT.Identity.UnitTests.ApiTests.Controllers;
public class PostsControllerTests
{
    private Mock<IMediator> mockMediator;
    private PostsController controller;

    public PostsControllerTests()
    {
        this.mockMediator = new Mock<IMediator>();
        this.controller = new PostsController(mockMediator.Object);
    }

    [Fact]
    public async Task ShouldPassPost()
    {
        mockMediator.Setup(c => c.Send(It.IsAny<dotnet_core_blogs_architecture.blogs.Mediator.Post.Commands.Create.CommandModel>(), default)).ReturnsAsync(AutoFixtureHelper.Fixture.Create<dotnet_core_blogs_architecture.Data.Results.CreatedResult>());
        var result = await controller.PostAsync(AutoFixtureHelper.Fixture.Create< dotnet_core_blogs_architecture.blogs.Mediator.Post.Commands.Create.CommandModel>());
        Assert.IsType<ObjectResult>(result);
    }

    [Fact]
    public async Task ShouldPassGetChargeCodes()
    {
        mockMediator.Setup(c => c.Send(It.IsAny<dotnet_core_blogs_architecture.blogs.Mediator.Post.Queries.List.QueryModel>(), default)).ReturnsAsync(AutoFixtureHelper.Fixture.Create<ValidObjectResult>());
        var result = await controller.GetPosts(AutoFixtureHelper.Fixture.Create<dotnet_core_blogs_architecture.blogs.Mediator.Post.Queries.List.QueryModel>());
        Assert.IsType<ObjectResult>(result);
    }

    [Fact]
    public async Task ShouldGetById()
    {
        mockMediator.Setup(c => c.Send(It.IsAny<dotnet_core_blogs_architecture.blogs.Mediator.Post.Queries.GetById.QueryModel>(), default)).ReturnsAsync(AutoFixtureHelper.Fixture.Create<ValidObjectResult>());
        var result = await controller.GetByIdAsync(AutoFixtureHelper.Fixture.Create<dotnet_core_blogs_architecture.blogs.Mediator.Post.Queries.GetById.QueryModel>().Id);
        Assert.IsType<ObjectResult>(result);
    }
}
