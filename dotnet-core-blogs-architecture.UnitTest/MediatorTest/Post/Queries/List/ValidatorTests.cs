using dotnet_core_blogs_architecture.blogs.Mediator.Post.Queries.List;
using FluentValidation.TestHelper;

namespace dotnet_core_blogs_architecture.UnitTest.MediatorTest.Post.Queries.List;
public class ValidatorTests
{
	private Validator validator;
	public ValidatorTests()
	{
		validator = new Validator();
	}

	string aboveHundredCharacter = "*e0CuA*WRNQvkddadsaajtM8Yw1!Oh0b!6u1gr3TKWpg=XO+DabUQScQEXAGREaovrvMgZwn!=X1T+$akuVdYO2thuEzH4wwX5QWNuv%AdO\r\n";
	[Fact]
	public void ShouldNotPassFromValidator()
	{
		validator.TestValidate(new QueryModel() { Page = -1, SortOrder = "ASC" }).ShouldHaveValidationErrorFor(x => x.Page);
		validator.TestValidate(new QueryModel() { PageSize = -1, SortOrder = "ASC" }).ShouldHaveValidationErrorFor(x => x.PageSize);
		validator.TestValidate(new QueryModel() { Search = aboveHundredCharacter, SortOrder = "ASC" }).ShouldHaveValidationErrorFor(x => x.Search);
		validator.TestValidate(new QueryModel() { SortCol = "abc", SortOrder = "ASC" }).ShouldHaveValidationErrorFor(x => x.SortCol);
	}

	[Fact]
	public void ShouldPassFromValidator()
	{		
		validator.TestValidate(new QueryModel() { Page = 0, SortOrder = "ASC" }).ShouldNotHaveValidationErrorFor(x => x.Page);
		validator.TestValidate(new QueryModel() { PageSize = 1, SortOrder = "ASC" }).ShouldNotHaveValidationErrorFor(x => x.PageSize);
		validator.TestValidate(new QueryModel() { Search = "Initial post", SortOrder = "ASC" }).ShouldNotHaveValidationErrorFor(x => x.Search);
		validator.TestValidate(new QueryModel() { SortCol = "Post", SortOrder = "ASC" }).ShouldNotHaveValidationErrorFor(x => x.SortCol);
	}
}
