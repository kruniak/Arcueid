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
    private readonly ArcueidContext _dbContext;
    private readonly IMapper _mapper;

    public DogService(ArcueidContext arcueidContext, IMapper mapper)
    {
        _dbContext = arcueidContext;
        _mapper = mapper;
    }

    public async Task<OperationResult<IEnumerable<DogBreedDto>>> GetDogBreedsAsync()
    {
        var dogBreeds = await _dbContext.DogBreeds
            .ProjectTo<DogBreedDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return dogBreeds;
    }

    public async Task<OperationResult<IEnumerable<DogDto>>> GetDogsAsync()
    {
        var dogs = await _dbContext.Dogs
            .ProjectTo<DogDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return dogs;
    }
    public async Task<OperationResult<IEnumerable<DogDto>>> GetDogsByNameAsync(string name)
    {
        var dogs = await _dbContext.Dogs
            .ProjectTo<DogDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return dogs;
    }

    public async Task<OperationResult<IEnumerable<DogDto>>> GetDogsByBreedAsync(DogBreedDto dogBreed)
    {
        var dogs = await _dbContext.Dogs
            .Where(d => d.Breed.Id == dogBreed.Id)
            .ProjectTo<DogDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return dogs;
    }

    public async Task<OperationResult<DogDto>> AddDogAsync(DogDto dogDto)
    {
        var dog = _mapper.Map<Dog>(dogDto);

        var newDog = _dbContext.Dogs.Add(dog).Entity;

        await _dbContext.SaveChangesAsync();

        return _mapper.Map<DogDto>(newDog);
    }
}
