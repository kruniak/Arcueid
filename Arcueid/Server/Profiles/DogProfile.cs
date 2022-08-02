using Arcueid.Server.Database.Entities;
using Arcueid.Shared.Dto;
using AutoMapper;

namespace Arcueid.Server.Profiles;

public class DogProfile : Profile
{
    public DogProfile()
    {
        CreateMap<Dog, DogDto>().ReverseMap();
    }
}
