using AutoMapper;
using dotnet_core_blogs_architecture.blogs.Mediator.Post.Shared;
using dotnet_core_blogs_architecture.infrastructure;

namespace dotnet_core_blogs_architecture.blogs.Mediator.Post
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Data.Models.Post, PostResponseModel>(MemberList.None);

            CreateMap<PaginatedList<Data.Models.Post>, PaginatedResponseModel<PostResponseModel>>(MemberList.None)
                .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src));
        }
    }
}
