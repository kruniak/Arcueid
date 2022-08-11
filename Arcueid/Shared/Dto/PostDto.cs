namespace Arcueid.Shared.Dto;

public class PostDto
{
    public int Id { get; set; }
    public string Author { get; set; } = null!;
    public string Text { get; set; } = null!;
    public ICollection<TagDto> Tags { get; set; } = null!;
}
