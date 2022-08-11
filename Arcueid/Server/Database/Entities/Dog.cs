using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Arcueid.Server.Database.Entities;

// (sort of) relevant NOTE regarding all entities: we could have nullable backing fields
//  accessible from non-nullable properties as described in this article:
//  https://docs.microsoft.com/en-us/ef/core/miscellaneous/nullable-reference-types
//  but initializing props with the null-forgiving operator is terser and simpler.
//  make sure it does not cause any issues

public class Dog
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Age { get; set; }

    [ForeignKey("DogBreedId")] public virtual DogBreed Breed { get; set; } = null!;
}

