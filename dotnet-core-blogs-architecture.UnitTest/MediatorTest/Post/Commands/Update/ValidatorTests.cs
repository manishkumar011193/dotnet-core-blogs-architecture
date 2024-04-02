using Ardalis.Specification;
using AutoFixture;
using dotnet_core_blogs_architecture.blogs.Mediator.Post.Commands.Update;
using dotnet_core_blogs_architecture.infrastructure.Interfaces;
using dotnet_core_blogs_architecture.UnitTest.Helpers;
using FluentValidation.TestHelper;
using Moq;

namespace DT.Identity.UnitTests.CoreTests.ChargeCode.Commands.Update;
public class ValidatorTests
{
	private Mock<IReadRepository<dotnet_core_blogs_architecture.Data.Models.Post>> mockChargeCodeRepository;
	private Validator validator;

	public ValidatorTests()
	{
		mockChargeCodeRepository = new Mock<IReadRepository<dotnet_core_blogs_architecture.Data.Models.Post>>();
		validator = new Validator(mockChargeCodeRepository.Object);
	}

	[Fact]
	public void ShouldPassPostId()
	{
		mockChargeCodeRepository.Setup(x => x.Any(It.IsAny<ISpecification<dotnet_core_blogs_architecture.Data.Models.Post>>())).Returns(false);
		validator.TestValidate(new CommandModel() { Id = 0 }).ShouldHaveValidationErrorFor(x => x.Id);
		validator.TestValidate(new CommandModel() { Id = -10 }).ShouldHaveValidationErrorFor(x => x.Id);
		mockChargeCodeRepository.Setup(x => x.Any(It.IsAny<ISpecification<dotnet_core_blogs_architecture.Data.Models.Post>>())).Returns(true);
		validator.TestValidate(new CommandModel() { Id = 1 }).ShouldNotHaveValidationErrorFor(x => x.Id);
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
        validator.TestValidate(new CommandModel() { Content = "InitiaLContext$%#" }).ShouldHaveValidationErrorFor(x => x.Content);
    }
}
