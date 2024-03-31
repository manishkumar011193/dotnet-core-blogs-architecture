using MediatR;
using DT.Shared.Repository.Interfaces;
using DT.Identity.Repository.Models;
using DT.Identity.Repository.Specifications.User;
using Microsoft.Extensions.Logging;
using DT.Shared.Web.Results;
using DT.Identity.Core.Country.Shared;

namespace DT.Identity.Core.User.Queries.AllMasterData;
public class Handler : IRequestHandler<QueryModel, ValidationResult>
{
	private readonly IReadRepository<DT.Identity.Repository.Models.Country> countryReadRepository;
	private readonly IReadRepository<Department> departmentReadRepository;
	private readonly IReadRepository<Designation> designationReadRepository;
	private readonly IReadRepository<Role> roleReadRepository;
	private readonly IReadRepository<UserType> userTypeReadRepository;
	private readonly IReadRepository<Repository.Models.User> userReadRepository;
	private readonly ILogger _logger;
	public Handler(IReadRepository<DT.Identity.Repository.Models.Country> countryReadRepository, 
		IReadRepository<Department> departmentReadRepository, 
		IReadRepository<Designation> designationReadRepository, 
		IReadRepository<Role> roleReadRepository,
		IReadRepository<UserType> userTypeReadRepository, 
		IReadRepository<Repository.Models.User> userReadRepository, ILogger<Handler> logger)
	{
		this.countryReadRepository = countryReadRepository;
		this.departmentReadRepository = departmentReadRepository;
		this.designationReadRepository = designationReadRepository;
		this.roleReadRepository = roleReadRepository;
		this.userTypeReadRepository = userTypeReadRepository;
		this.userReadRepository = userReadRepository;
		_logger = logger;
	}
	public async Task<ValidationResult> Handle(QueryModel query, CancellationToken cancellationToken)
	{
		_logger.LogInformation("Fetching Master Data");
		var departments = await this.departmentReadRepository.ListAsync(cancellationToken);
		var designations = await this.designationReadRepository.ListAsync(cancellationToken);
		var countries = await this.countryReadRepository.ListAsync(cancellationToken);
		var userTypes = await this.userTypeReadRepository.ListAsync(cancellationToken);
		var userRoles = await this.roleReadRepository.ListAsync(cancellationToken);
		var reportinguser = await this.userReadRepository.ListAsync(new ReportingManagerWithSpecification());
		var userdetails = new ResponseModel
		{
			 Departments = departments.Select(x => new Common.DepartmentResponseModel { Id = x.Id, Name = x.Name }).ToList(),
			 Countries = countries.Select(x => new CountryResponseModel { Id = x.Id, Name = x.Name, Code = x.Code, ISOCode = x.ISOCode }).ToList(),
			 Designations = designations.Select(x => new Common.DesignationResponseModel { Id = x.Id, Name = x.Name }).ToList(),
			 UserRoles = userRoles.Select(x => new Common.RoleResponseModel { Id = x.Id, Name = x.Name }).ToList(),
			 UserTypes = userTypes.Select(x => new Common.UserTypeResponseModel { Id = x.Id, Name = x.Name }).ToList(),
			 ReportingManagers = reportinguser.Select(x => new Common.ReportingManagerResponseModel { Id = x.Id, Name = $"{x.FirstName} {x.LastName}", DepartmentId = x.DepartmentId!.Value, DepartmentName = x.Department.Name }).ToList(),
		}; 
		_logger.LogInformation("Successfully fetched master data");
		return new ValidObjectResult(userdetails);
	}
}
