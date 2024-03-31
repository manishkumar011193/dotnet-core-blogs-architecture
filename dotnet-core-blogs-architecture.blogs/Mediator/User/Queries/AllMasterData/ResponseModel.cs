using DT.Identity.Core.Common;
using DT.Identity.Core.Country.Shared;

namespace DT.Identity.Core.User.Queries.AllMasterData;
public class ResponseModel
{
	public List<DepartmentResponseModel> Departments { get; set; } = new List<DepartmentResponseModel>();

	public List<DesignationResponseModel> Designations { get; set; } = new List<DesignationResponseModel>();

	public List<Country.Shared.CountryResponseModel> Countries { get; set; } = new List<Country.Shared.CountryResponseModel>();

	public List<UserTypeResponseModel> UserTypes { get; set; } = new List<UserTypeResponseModel>();

	public List<RoleResponseModel> UserRoles { get; set; } = new List<RoleResponseModel>();

	public List<ReportingManagerResponseModel> ReportingManagers { get; set; } = new List<ReportingManagerResponseModel>();
}
