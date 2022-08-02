using Arcueid.Server.Core;
using Arcueid.Server.Database;
using Arcueid.Server.Database.Entities;
using Arcueid.Shared.Dto;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Arcueid.Server.Services;

public class DogService : IDogService
{
    private readonly ArcueidContext dbContext;
    private readonly IMapper mapper;

    public DogService(ArcueidContext arcueidContext, IMapper mapper)
    {
        dbContext = arcueidContext;
        this.mapper = mapper;
    }

    public async Task<OperationResult<IEnumerable<DogBreedDto>?>> GetDogBreedsAsync()
    {
        var dogBreeds = await dbContext.DogBreeds
            .ProjectTo<DogBreedDto>(mapper.ConfigurationProvider)
            .ToListAsync();

        return dogBreeds;
    }

    public async Task<OperationResult<IEnumerable<DogDto>?>> GetDogsAsync()
    {
        var dogs = await dbContext.Dogs
            .ProjectTo<DogDto>(mapper.ConfigurationProvider)
            .ToListAsync();

        return dogs;
    }

    public async Task<OperationResult<IEnumerable<DogDto>?>> GetDogsByBreedAsync(DogBreedDto dogBreed)
    {
        var dogs = await dbContext.Dogs
            .Where(d => d.Breed.Id == dogBreed.Id)
            .ProjectTo<DogDto>(mapper.ConfigurationProvider)
            .ToListAsync();

        return dogs;
    }

    public async Task<OperationResult<DogDto?>> AddDogAsync(DogDto dogDto)
    {
        if (dogDto == null)
        {
            return OperationResult.Fail(FailureReason.ClientError);
        }

        var dog = mapper.Map<Dog>(dogDto);

        var newDog = dbContext.Dogs.Add(dog).Entity;

        await dbContext.SaveChangesAsync();

        return mapper.Map<DogDto>(newDog);
    }
}
