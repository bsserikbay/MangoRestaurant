using Mango.Servces.ProductAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Mango.Servces.ProductAPI.DbContexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 1,
                Name = "Samosa",
                Price = 15,
                Description = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Maxime mollitia, eaque rerum! Provident si",
                ImageUrl = "https://dotnetmastery312.blob.core.windows.net/mango/1.jfif",
                CategoryName = "Appetizer"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 2,
                Name = "Paneer Tika",
                Price = 13.99,
                Description = " Ipsa laudantium molestias eos sapiente officiis modi at sunt excepturi expedita sint",
                ImageUrl = "https://dotnetmastery312.blob.core.windows.net/mango/2.jfif",
                CategoryName = "Appetizer"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 3,
                Name = "Sweet Pie",
                Price = 10.99,
                Description = " IPerspiciatis minima nesciunt dolorem! Officiis iure rerum voluptates a cumque velitquibusdam sed amet tempora.Sit laborum ab,eius fugit doloribus tenetur",
                ImageUrl = "https://dotnetmastery312.blob.core.windows.net/mango/3.jfif",
                CategoryName = "Dessert"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 4,
                Name = "Pav Bhaji",
                Price = 15,
                Description = "Ipsa laudantium molestias eos sapiente officiis modi at sunt excepturi expedita sint ? Sed quibusdam recusandae alias error harum maxime adipisci amet laborum.Perspiciatis minima nesciunt dolorem! Officiis iure rerum voluptates a cumque velit quibusdam sed amet tempora.Sit laborum ab eius fugit doloribus teetur fugiat, temporibus enim commodi iusto",
                ImageUrl = "https://dotnetmastery312.blob.core.windows.net/mango/4.jfif",
                CategoryName = "Entree"
            });
        }
    }
}
