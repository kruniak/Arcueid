using Arcueid.Server.Database.Entities;
using Arcueid.Shared.Dto;
using AutoMapper;

namespace Arcueid.Server.Profiles;

public class TagProfile : Profile
{
    public TagProfile()
    {
        CreateMap<Tag, TagDto>().ReverseMap();
    }
}
