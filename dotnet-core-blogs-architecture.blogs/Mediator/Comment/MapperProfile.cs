﻿using AutoMapper;
using dotnet_core_blogs_architecture.blogs.Mediator.Comment.Shared;
using dotnet_core_blogs_architecture.infrastructure;

namespace dotnet_core_blogs_architecture.blogs.Mediator.Comment
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Data.Models.Comment, CommentResponseModel>(MemberList.None);
            CreateMap<PaginatedList<Data.Models.Comment>, PaginatedResponseModel<CommentResponseModel>>(MemberList.None)
                  .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src));
        }
    }
}
