using AutoMapper;
using dotnet_core_blogs_architecture.blogs.Mediator.User.Shared;
using dotnet_core_blogs_architecture.infrastructure;

namespace dotnet_core_blogs_architecture.blogs.Mediator.User;
public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Data.Models.User, UserResponseModel>(MemberList.None);
        CreateMap<PaginatedList<Data.Models.User>, PaginatedResponseModel<UserResponseModel>>(MemberList.None)
              .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src));
    }
}