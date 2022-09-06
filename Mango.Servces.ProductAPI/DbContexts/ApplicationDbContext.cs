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
                ImageUrl = "https://food.fnr.sndimg.com/content/dam/images/food/products/2022/3/11/rx_goldbelly-clinton-street-diner-zeus-burger.jpg.rend.hgtvcom.406.305.suffix/1647019464547.jpeg",
                CategoryName = "Appetizer"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 2,
                Name = "Paneer Tika",
                Price = 13.99,
                Description = " Ipsa laudantium molestias eos sapiente officiis modi at sunt excepturi expedita sint",
                ImageUrl = "https://cdn.cnn.com/cnnnext/dam/assets/200811115525-04-best-polish-foods-super-169.jpg",
                CategoryName = "Appetizer"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 3,
                Name = "Sweet Pie",
                Price = 10.99,
                Description = " IPerspiciatis minima nesciunt dolorem! Officiis iure rerum voluptates a cumque velitquibusdam sed amet tempora.Sit laborum ab,eius fugit doloribus tenetur",
                ImageUrl = "https://i.ytimg.com/vi/R8Y7NWC5jgM/maxresdefault.jpg",
                CategoryName = "Dessert"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 4,
                Name = "Pav Bhaji",
                Price = 15,
                Description = "Ipsa laudantium molestias eos sapiente officiis modi at sunt excepturi expedita sint ? Sed quibusdam recusandae alias error harum maxime adipisci amet laborum.Perspiciatis minima nesciunt dolorem! Officiis iure rerum voluptates a cumque velit quibusdam sed amet tempora.Sit laborum ab eius fugit doloribus teetur fugiat, temporibus enim commodi iusto",
                ImageUrl = "https://cdn.cnn.com/cnnnext/dam/assets/220719164934-01-inexpensive-food-healthy-stock-super-tease.jpeg",
                CategoryName = "Entree"
            });
        }
    }
}
