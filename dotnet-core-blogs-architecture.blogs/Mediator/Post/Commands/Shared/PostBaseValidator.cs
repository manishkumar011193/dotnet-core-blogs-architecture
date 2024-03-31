//using System.Text.RegularExpressions;
//using DT.Identity.Core.Common;
//using DT.Shared.Repository.Interfaces;
//using FluentValidation;
//using DT.Identity.Core.Port.Commands.Shared;

//namespace dotnet_core_blogs_architecture.blogs.Post.Commands.Shared;
//public abstract class PostBaseValidator<T> : AbstractValidator<T> where T : PostBaseCommandModel
//{
//	protected readonly IReadRepository<Repository.Models.Agent> agentRepository;
//	protected readonly IReadRepository<Repository.Models.AgentAddress> agentAddressRepository;
//	protected readonly IReadRepository<Repository.Models.AgentPort> agentPortRepository;
//	protected readonly IReadRepository<Repository.Models.Port> portRepository;
//	protected readonly IReadRepository<Repository.Models.Country> countryRepository;
//	protected readonly Regex onlyAllowWordsWithSpace = new Regex(RegexConstants.OnlyAllowWordsWithSpace);

//	public PostBaseValidator(IReadRepository<Repository.Models.Agent> agentRepository, IReadRepository<Repository.Models.AgentAddress> agentAddressRepository,
//		IReadRepository<Repository.Models.AgentPort> agentPortRepository, IReadRepository<Repository.Models.Port> portRepository, IReadRepository<Repository.Models.Country> countryRepository)
//	{
//		this.agentRepository = agentRepository;
//		this.agentAddressRepository = agentAddressRepository;
//		this.agentPortRepository = agentPortRepository;
//		this.portRepository = portRepository;
//		this.countryRepository = countryRepository;
//		RuleFor(x => x).NotEmpty().NotNull();
//		RuleFor(x => x.Name).NotEmpty().NotNull().MinimumLength(1).MaximumLength(100).Matches(onlyAllowWordsWithSpace).WithMessage("Agent name can only contains words with space.");
//		RuleFor(x => x.Code).NotEmpty().NotNull().MinimumLength(1).MaximumLength(20);
//		RuleFor(x => x.Addresses).Must(Addresses => Addresses != null && Addresses.Any()).WithMessage("There must be at least one address.");
//		RuleFor(x => x.Countries).NotEmpty().NotNull().Must(z => z != null && z.Count > 0);
//		RuleFor(x => x.Countries).SetValidator(new CountryPortCommandValidator(countryRepository)).When(x => x != null && x.Countries != null && x.Countries.Count > 0);	
//		RuleFor(x => x.IsActive).NotEmpty().NotNull().WithMessage("Please select the status of the Agent.");
//	}
//}
