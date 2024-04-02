using Ardalis.Specification;
using AutoFixture;
using dotnet_core_blogs_architecture.blogs.Mediator.Post.Commands.Create;
using dotnet_core_blogs_architecture.infrastructure.Interfaces;
using dotnet_core_blogs_architecture.UnitTest.Helpers;
using FluentValidation.TestHelper;
using Moq;

namespace dotnet_core_blogs_architecture.UnitTest.MediatorTest.Post.Commands.Create;
public class ValidatorTests
{
	private Mock<IReadRepository<Data.Models.Post>> mockPostRepository;
	private Validator validator;
	public ValidatorTests()
	{
		mockPostRepository = new Mock<IReadRepository<Data.Models.Post>>();
		this.validator = new Validator(mockPostRepository.Object);
	}

	[Fact]
	public void ShouldPassPostTitle()
	{
		validator.TestValidate(new CommandModel() { Title = "It is initial Title of the Post" }).ShouldNotHaveValidationErrorFor(x => x.Title);
		validator.TestValidate(new CommandModel() { Title = "Itisinitial" }).ShouldNotHaveValidationErrorFor(x => x.Title);
		validator.TestValidate(new CommandModel() { Title = "It is initial Title of the Post 1234" }).ShouldNotHaveValidationErrorFor(x => x.Title);
		validator.TestValidate(new CommandModel() { Title = "" }).ShouldHaveValidationErrorFor(x => x.Title);
		validator.TestValidate(new CommandModel() { Title = null }).ShouldHaveValidationErrorFor(x => x.Title);
		validator.TestValidate(new CommandModel() { Title = string.Join(string.Empty, AutoFixtureHelper.Fixture.CreateMany<char>(51)) }).ShouldHaveValidationErrorFor(x => x.Title);
		validator.TestValidate(new CommandModel() { Title = "It is initial $%#" }).ShouldNotHaveValidationErrorFor(x => x.Title);
	}
	
	[Fact]
	public void ShouldPassPostContext()
	{
		validator.TestValidate(new CommandModel() { Content = "Initial Post Context" }).ShouldNotHaveValidationErrorFor(x => x.Content);		
		validator.TestValidate(new CommandModel() { Content = "Initial Post Context123" }).ShouldHaveValidationErrorFor(x => x.Content);
		validator.TestValidate(new CommandModel() { Content = "" }).ShouldHaveValidationErrorFor(x => x.Content);
		validator.TestValidate(new CommandModel() { Content = null }).ShouldHaveValidationErrorFor(x => x.Content);
		validator.TestValidate(new CommandModel() { Content = string.Join(string.Empty, AutoFixtureHelper.Fixture.CreateMany<char>(201)) }).ShouldHaveValidationErrorFor(x => x.Content);
		validator.TestValidate(new CommandModel() { Content	 = "InitiaLContext$%#" }).ShouldHaveValidationErrorFor(x => x.Content);
	}
}
