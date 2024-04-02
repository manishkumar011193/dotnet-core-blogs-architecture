using dotnet_core_blogs_architecture.blogs.Mediator.Post.Queries.GetById;
using FluentValidation.TestHelper;

namespace dotnet_core_blogs_architecture.UnitTest.MediatorTest.Post.Queries.PostById;
public class ValidatorTests
{
	private Validator validator;

	public ValidatorTests()
	{
		validator = new Validator();
	}

	[Fact]
	public void ShouldPassPostId()
	{
		validator.TestValidate(new QueryModel() { Id = 0 }).ShouldHaveValidationErrorFor(x => x.Id);
		validator.TestValidate(new QueryModel() { Id = -40 }).ShouldHaveValidationErrorFor(x => x.Id);
		validator.TestValidate(new QueryModel() { Id = 2 }).ShouldNotHaveValidationErrorFor(x => x.Id);
	}
}
