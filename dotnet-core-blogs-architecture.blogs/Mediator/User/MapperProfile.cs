using AutoMapper;
using DT.Identity.Core.Common;
using DT.Identity.Core.User.Shared;
using DT.Identity.Repository.Models;
using DT.Shared.Repository;

namespace DT.Identity.Core.User;
public class MapperProfile : Profile
{
	public MapperProfile()
	{
		CreateMap<Department, DepartmentResponseModel>(MemberList.None);
		CreateMap<Designation, DesignationResponseModel>(MemberList.None);
		CreateMap<Repository.Models.User, ReportingManagerResponseModel>(MemberList.None)
			.ForMember(dest => dest.DepartmentName, opt => opt.MapFrom((src, dest) =>
			{
				return $"{src.Department?.Name}";
			}))
			.ForMember(dest => dest.Name, opt => opt.MapFrom((src, dest) =>
			{
				return src.GetFullName();
				
			}))
			.ForMember(dest => dest.DepartmentId, opt => opt.MapFrom((src, dest) =>
			{
				return src.DepartmentId;
			}))
			.ForMember(dest => dest.Id, opt => opt.MapFrom((src, dest) =>
			{
				return src.Id;
			}));
	//	CreateMap<Role, RoleResponseModel>(MemberList.None);
		CreateMap<UserRole, RoleResponseModel>(MemberList.None)
			.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Role.Id))
			.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Role.Name));
		CreateMap<UserType, UserTypeResponseModel>(MemberList.None);
		CreateMap<Repository.Models.User, UserResponseModel>(MemberList.None);
		CreateMap<PaginatedList<Repository.Models.User>, PaginatedResponseModel<UserResponseModel>>(MemberList.None)
			.ForMember(dest => dest.Data, opt => opt.MapFrom(src => src));
	}
}