using Arcueid.Server.Database.Entities;
using Arcueid.Shared.Dto;
using AutoMapper;

namespace Arcueid.Server.Profiles;

public class PostProfile : Profile
{
    public PostProfile()
    {
        CreateMap<Post, PostDto>().ReverseMap();
    }
}
