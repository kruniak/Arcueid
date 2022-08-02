namespace Arcueid.Shared.Dto;

public class DogDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public DogBreedDto Breed { get; set; }
}
