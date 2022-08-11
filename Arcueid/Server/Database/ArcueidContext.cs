using Arcueid.Server.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Arcueid.Server.Database;

public class ArcueidContext : DbContext
{
    public DbSet<Dog> Dogs => Set<Dog>();
    public DbSet<DogBreed> DogBreeds => Set<DogBreed>();

    public ArcueidContext(DbContextOptions<ArcueidContext> options)
        : base(options) {}
}
