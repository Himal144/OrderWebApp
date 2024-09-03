using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OrderWebApp.Models;

namespace OrderWebApp.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Fashion", DisplayOrder = 1 },
                new Category { Id = 2, Name = "Electronics", DisplayOrder = 2 },
                new Category { Id = 3, Name = "Gadgets", DisplayOrder = 3 }
                );

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Title = "Ek Chihan",
                    Description = "A gripping Nepali novel that explores human emotions.",
                    ISBN = "978-9937-888-01-1",
                    Author = "Madan Krishna Shrestha",
                    ListPrice = 500,
                    Price = 450,
                    Price50 = 425,
                    Price100 = 400,
                    CategoryId=2,
                    ImageUrl=""
                },
                new Product
                {
                    Id = 2,
                    Title = "Karnali Blues",
                    Description = "A popular Nepali novel about life in rural Nepal.",
                    ISBN = "978-9937-888-02-2",
                    Author = "Budhathoki Krishna",
                    ListPrice = 600,
                    Price = 550,
                    Price50 = 525,
                    Price100 = 500,
                    CategoryId = 1,
                    ImageUrl = ""
                },
                new Product
                {
                    Id = 3,
                    Title = "Palpasa Café",
                    Description = "A novel set during the Nepalese civil war.",
                    ISBN = "978-9937-888-03-3",
                    Author = "Narayan Wagle",
                    ListPrice = 700,
                    Price = 650,
                    Price50 = 625,
                    Price100 = 600,
                    CategoryId = 3,
                    ImageUrl = ""
                },
                new Product
                {
                    Id = 4,
                    Title = "Summer Love",
                    Description = "A modern Nepali love story.",
                    ISBN = "978-9937-888-04-4",
                    Author = "Subin Bhattarai",
                    ListPrice = 400,
                    Price = 375,
                    Price50 = 350,
                    Price100 = 325,
                    CategoryId = 2,
                    ImageUrl = ""
                },
                new Product
                {
                    Id = 5,
                    Title = "Seto Dharti",
                    Description = "A novel about life in the early 20th century Nepal.",
                    ISBN = "978-9937-888-05-5",
                    Author = "Amar Neupane",
                    ListPrice = 550,
                    Price = 500,
                    Price50 = 475,
                    Price100 = 450,
                    CategoryId = 1,
                    ImageUrl = ""
                },
                new Product
                {
                    Id = 6,
                    Title = "Sirish Ko Phool",
                    Description = "A novel exploring human relationships and society.",
                    ISBN = "978-9937-888-06-6",
                    Author = "Parijat",
                    ListPrice = 350,
                    Price = 325,
                    Price50 = 300,
                    Price100 = 275,
                    CategoryId = 1,
                    ImageUrl = ""
                }

                );
        }
    }
}
