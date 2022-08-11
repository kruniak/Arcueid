using Arcueid.Server.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Arcueid.Server.Database;

public class ArcueidContext : DbContext
{
    public DbSet<Dog> Dogs => Set<Dog>();
    public DbSet<DogBreed> DogBreeds => Set<DogBreed>();
    public DbSet<Post> Posts => Set<Post>();
    public DbSet<Tag> Tags => Set<Tag>();

    public ArcueidContext(DbContextOptions<ArcueidContext> options)
        : base(options) {}
}
