namespace Arcueid.Shared.Dto;

public class TagDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public ICollection<PostDto> Posts { get; set; } = null!;
}
