using Microsoft.EntityFrameworkCore;

namespace DogTestApi.Models
{
    public class DogsContext : DbContext
    {
        public DbSet<Dog> Dogs { get; set; }
        public DbSet<Dog> TestDogs { get; set; }
        public DogsContext(DbContextOptions<DogsContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
