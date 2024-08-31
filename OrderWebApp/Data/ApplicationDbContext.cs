using Microsoft.EntityFrameworkCore;
using OrderWebApp.Models;

namespace OrderWebApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Fashion", DisplayOrder = 1 },
                new Category { Id = 2, Name = "Electronics", DisplayOrder = 2 },
                new Category { Id = 3, Name = "Gadgets", DisplayOrder = 3 }
                );
        }
    }
}
