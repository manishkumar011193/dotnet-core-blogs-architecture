using Ardalis.Specification;
using AutoFixture;
using dotnet_core_blogs_architecture.blogs.Mediator.Post.Commands.Update;
using dotnet_core_blogs_architecture.blogs.Mediator.Post.Shared;
using dotnet_core_blogs_architecture.Data.Results;
using dotnet_core_blogs_architecture.infrastructure.Data;
using dotnet_core_blogs_architecture.UnitTest.Helpers;
using Microsoft.Extensions.Logging;
using Moq;

namespace DT.Identity.UnitTests.CoreTests.ChargeCode.Commands.Update;
public class HandlerTests
{
	private Handler handler;
	private Mock<IRepository<dotnet_core_blogs_architecture.Data.Models.Post>> mockPostRepository;
	private Mock<ILogger<Handler>> mockLogger;
	private CancellationToken cancellation = CancellationToken.None;

	public HandlerTests()
	{
		mockPostRepository = new Mock<IRepository<dotnet_core_blogs_architecture.Data.Models.Post>>();
		mockLogger = new Mock<ILogger<Handler>>();
		handler = new Handler(mockPostRepository.Object, MapperHelper.Instance, mockLogger.Object);
	}

	[Fact]
	public async Task ShouldPassHandler()
	{
		var dbPost = AutoFixtureHelper.Fixture.Build<dotnet_core_blogs_architecture.Data.Models.Post>()
			.With(c => c.Id, 1)
			.With(c => c.Title, "Initial Post")
			.With(c => c.Content, "Initial Context")
			.Create();

		var updatecommand = AutoFixtureHelper.Fixture.Build<CommandModel>()
            .With(c => c.Id, 1)
            .With(c => c.Title, "Initial Post")
            .With(c => c.Content, "Initial Context")
            .Create();

        mockPostRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<ISpecification<dotnet_core_blogs_architecture.Data.Models.Post>>(), cancellation)).ReturnsAsync(dbPost);
		mockPostRepository.Setup(x => x.SaveChangesAsync(cancellation)).ReturnsAsync(1);
		var result = await handler.Handle(updatecommand, cancellation);
		mockPostRepository.Verify(x => x.FirstOrDefaultAsync(It.IsAny<ISpecification<dotnet_core_blogs_architecture.Data.Models.Post>>(), cancellation), Times.Exactly(1));
		mockPostRepository.Verify(x => x.SaveChangesAsync(cancellation));

		mockPostRepository.Verify(x => x.UpdateAsync(It.Is<dotnet_core_blogs_architecture.Data.Models.Post>(c =>
			c.Id == updatecommand.Id &&
			c.Content.Equals(updatecommand.Content) &&
			c.Title.Equals(updatecommand.Title)
		), cancellation), Times.Once);

		Assert.IsType<ValidObjectResult>(result);
		var result1 = (result as ValidObjectResult).Data as PostResponseModel;
		Assert.Equal(updatecommand.Id, result1.Id);
		Assert.Equal(updatecommand.Content, result1.Content);
		Assert.Equal(updatecommand.Content.ToUpper(), result1.Content);		
	}

	[Fact]
	public async Task ShouldNotPassHandler()
	{
		var command = AutoFixtureHelper.Fixture.Create<CommandModel>();
		mockPostRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<ISpecification<dotnet_core_blogs_architecture.Data.Models.Post>>(), cancellation));
		var result = await handler.Handle(command, cancellation);
		mockPostRepository.Verify(x => x.FirstOrDefaultAsync(It.IsAny<ISpecification<dotnet_core_blogs_architecture.Data.Models.Post>>(), cancellation), Times.Once);
		mockPostRepository.Verify(x => x.UpdateAsync(It.IsAny<dotnet_core_blogs_architecture.Data.Models.Post>(), cancellation), Times.Never);

		Assert.IsType<NotFoundValidationResult>(result);
		var result1 = result as NotFoundValidationResult;

		Assert.NotNull(result1);
		Assert.Contains(result.Errors, x => x.Field == "PostId");
		Assert.Contains(result.Errors, x => x.Description[0] == "Invalid Post Id");
	}

	[Fact]
	public async Task ShouldPassHandlerForNoDataUpdate()
	{
		var dbChargeCode = AutoFixtureHelper.Fixture.Build<dotnet_core_blogs_architecture.Data.Models.Post>()
			.With(c => c.Id, 1)
			.With(c => c.Content, "Initial Context of the Post")
			.With(c => c.Title, "Initil Post")		
			.Create();

		var updatecommand = AutoFixtureHelper.Fixture.Build<CommandModel>()
            .With(c => c.Id, 1)
            .With(c => c.Content, "Initial Context of the Post")
            .With(c => c.Title, "Initil Post")            
			.Create();
		mockPostRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<ISpecification<dotnet_core_blogs_architecture.Data.Models.Post>>(), cancellation)).ReturnsAsync(dbChargeCode);
		var result = await handler.Handle(updatecommand, cancellation);
		mockPostRepository.Verify(x => x.FirstOrDefaultAsync(It.IsAny<ISpecification<dotnet_core_blogs_architecture.Data.Models.Post>>(), cancellation), Times.Exactly(1));
		mockPostRepository.Verify(x => x.SaveChangesAsync(cancellation));
		mockPostRepository.Verify(x => x.UpdateAsync(It.Is<dotnet_core_blogs_architecture.Data.Models.Post>(c =>
			c.Id == updatecommand.Id &&
			c.Content.Equals(updatecommand.Content) &&
			c.Title.Equals(updatecommand.Title)
		), cancellation), Times.Once);

		Assert.IsType<InvalidValidationResult>(result);
		var errors = (result as InvalidValidationResult).Errors;
		Assert.Equal(1, errors.Count);
		Assert.Equal("Summary", errors[0].Field);
		Assert.Single(errors[0].Description);
		Assert.Equal("There was a server error updating 'ChargeCode'", errors[0].Description[0]);
	}
}
