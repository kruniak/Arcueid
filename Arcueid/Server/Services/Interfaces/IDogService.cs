using Arcueid.Server.Core;
using Arcueid.Shared.Dto;

namespace Arcueid.Server.Services;

public interface IDogService
{
    Task<OperationResult<IEnumerable<DogDto>>> GetDogsAsync();
    Task<OperationResult<IEnumerable<DogDto>>> GetDogsByNameAsync(string N);
    Task<OperationResult<IEnumerable<DogBreedDto>>> GetDogBreedsAsync();
    Task<OperationResult<IEnumerable<DogDto>>> GetDogsByBreedAsync(DogBreedDto dogBreed);
    Task<OperationResult<DogDto>> AddDogAsync(DogDto dogDto);
}
