using Arcueid.Server.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Arcueid.Server.Database;

public class ArcueidContext : DbContext
{
    public DbSet<Dog> Dogs { get; set; }
    public DbSet<DogBreed> DogBreeds { get; set; }

    public ArcueidContext(DbContextOptions<ArcueidContext> options)
        : base(options) {}
}
