using AutoFixture;
using dotnet_core_blogs_architecture.blogs.Mediator.Post.Queries.List;
using dotnet_core_blogs_architecture.blogs.Mediator.Post.Shared;
using dotnet_core_blogs_architecture.blogs.Repository.Specifications;
using dotnet_core_blogs_architecture.Data.Results;
using dotnet_core_blogs_architecture.infrastructure;
using dotnet_core_blogs_architecture.infrastructure.Interfaces;
using dotnet_core_blogs_architecture.UnitTest.Helpers;
using Microsoft.Extensions.Logging;
using Moq;

namespace dotnet_core_blogs_architecture.UnitTest.MediatorTest.Post.Queries.List;
public class HandlerTests
{
	private Handler handler;
	private Mock<IReadRepository<Data.Models.Post>> mockPostRepository;
	private Mock<ILogger<Handler>> mockLogger;
	private CancellationToken cancellation = CancellationToken.None;

	public HandlerTests()
	{
		mockPostRepository = new Mock<IReadRepository<Data.Models.Post>>();
		mockLogger = new Mock<ILogger<Handler>>();
        handler = new Handler(MapperHelper.Instance, mockPostRepository.Object, mockLogger.Object);
    }

	[Fact]
	public async Task ShouldPassForQuery()
	{
		var command = AutoFixtureHelper.Fixture.Build<QueryModel>()
			.With(c => c.Page, 1)
			.With(c => c.PageSize, 25)
			.With(c => c.All, false)
			.With(c => c.Search, "Initial Post")			
			.With(c => c.SortCol, "Post")
			.With(c => c.SortOrder, "desc")
			.Create();

		var paginatedResult = new PaginatedList<Data.Models.Post>(AutoFixtureHelper.Fixture.Build<Data.Models.Post>().CreateMany(5).ToList(), 10, 1, 1);
		mockPostRepository.Setup(c => c.GetPagninated(It.IsAny<PaginatedPostSpecification>(), It.IsAny<PageQueryModel>())).ReturnsAsync(paginatedResult);
		var result = await handler.Handle(command, cancellation);

		mockPostRepository.Verify(c => c.GetPagninated(It.Is<PaginatedPostSpecification>(c =>
			c.PageQueryModel.Page == command.Page &&
			c.PageQueryModel.PageSize == command.PageSize &&
			c.PageQueryModel.All == command.All &&
			c.PageQueryModel.Search == command.Search &&
			c.PageQueryModel.SortCol == command.SortCol &&
			c.PageQueryModel.SortOrder == command.SortOrder
		), It.Is<PageQueryModel>(c =>
			c.Page == command.Page &&
			c.PageSize == command.PageSize &&
			c.All == command.All &&
			c.Search == command.Search &&
			c.SortCol == command.SortCol &&
			c.SortOrder == command.SortOrder
		)));

		Assert.IsType<ValidObjectResult>(result);
		Assert.IsType<PaginatedResponseModel<PostResponseModel>>((result as ValidObjectResult).Data);
		var results = (result as ValidObjectResult).Data as PaginatedResponseModel<PostResponseModel>;
		Assert.Equal(paginatedResult.Count, results.Data.Count);
		for (var i = 0; i < results.Data.Count; i++)
		{
			Assert.Equal(paginatedResult[i].Id, results.Data[i].Id);
			Assert.Equal(paginatedResult[i].Title, results.Data[i].Title);
			Assert.Equal(paginatedResult[i].Content, results.Data[i].Content);		
		}
	}
}
