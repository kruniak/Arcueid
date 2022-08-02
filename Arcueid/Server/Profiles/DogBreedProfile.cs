using Arcueid.Server.Database.Entities;
using Arcueid.Shared.Dto;
using AutoMapper;

namespace Arcueid.Server.Profiles;

public class DogBreedProfile : Profile
{
    public DogBreedProfile()
    {
        CreateMap<DogBreed, DogBreedDto>().ReverseMap();
    }
}

