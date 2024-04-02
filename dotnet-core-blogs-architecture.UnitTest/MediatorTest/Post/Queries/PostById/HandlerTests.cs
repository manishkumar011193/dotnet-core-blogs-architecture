using Ardalis.Specification;
using AutoFixture;
using dotnet_core_blogs_architecture.blogs.Mediator.Post.Queries.GetById;
using dotnet_core_blogs_architecture.blogs.Mediator.Post.Shared;
using dotnet_core_blogs_architecture.Data.Results;
using dotnet_core_blogs_architecture.infrastructure.Interfaces;
using dotnet_core_blogs_architecture.UnitTest.Helpers;
using Microsoft.Extensions.Logging;
using Moq;

namespace dotnet_core_blogs_architecture.UnitTest.MediatorTest.Post.Queries.PostById;
public class HandlerTests
{
	private Handler _handler;
	private Mock<IReadRepository<Data.Models.Post>> mockPostRepository;
	private Mock<ILogger<Handler>> mockLogger;   

    public HandlerTests()
	{
		mockPostRepository = new Mock<IReadRepository<Data.Models.Post>>();		
        mockLogger = new Mock<ILogger<Handler>>();
		_handler = new Handler(
			mockPostRepository.Object,
			MapperHelper.Instance,
            mockLogger.Object);
	}

	[Fact]
	public async void ShouldPassHandler()
	{
		var dbPost = AutoFixtureHelper.Fixture.Build<Data.Models.Post>()			
			.Create();
		mockPostRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<ISpecification<Data.Models.Post>>(), CancellationToken.None)).ReturnsAsync(dbPost);
		mockPostRepository.Setup(x => x.Any(It.IsAny<ISpecification<Data.Models.Post>>())).Returns(true);
		var result = await _handler.Handle(new QueryModel() { Id = 1 }, CancellationToken.None);
		mockPostRepository.Verify(x => x.FirstOrDefaultAsync(It.IsAny<ISpecification<Data.Models.Post>>(), It.IsAny<CancellationToken>()));
		Assert.IsType<ValidObjectResult>(result);
		Assert.IsType<PostResponseModel>((result as ValidObjectResult).Data);
		var result1 = (result as ValidObjectResult).Data as PostResponseModel;
		Assert.Equal(dbPost.Id, result1.Id);
		Assert.Equal(dbPost.Title, result1.Title);
		Assert.Equal(dbPost.Content, result1.Content);		
	}

	[Fact]
	public async void ShouldNotPassHandler()
	{
		var dbChargeCode = AutoFixtureHelper.Fixture.Build<Data.Models.Post>().Without(c => c.Id).Create();
		dbChargeCode = null;
		mockPostRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<ISpecification<Data.Models.Post>>(), CancellationToken.None)).ReturnsAsync(dbChargeCode);
		mockPostRepository.Setup(x => x.Any(It.IsAny<ISpecification<Data.Models.Post>>())).Returns(true);
		var result = await _handler.Handle(new QueryModel() { Id = 1 }, CancellationToken.None);
		mockPostRepository.Verify(x => x.FirstOrDefaultAsync(It.IsAny<ISpecification<Data.Models.Post>>(), It.IsAny<CancellationToken>()));

		Assert.IsType<NotFoundValidationResult>(result);
		var result1 = result as NotFoundValidationResult;

		Assert.NotNull(result1);
		Assert.Contains(result.Errors, x => x.Field == "ChargeCodeId");
		Assert.Contains(result.Errors, x => x.Description[0] == "Invalid ChargeCode Id");
	}
}
