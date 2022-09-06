using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Mango.Servces.ProductAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryName", "Description", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Appetizer", "Lorem ipsum dolor sit amet consectetur adipisicing elit. Maxime mollitia, eaque rerum! Provident si", "https://food.fnr.sndimg.com/content/dam/images/food/products/2022/3/11/rx_goldbelly-clinton-street-diner-zeus-burger.jpg.rend.hgtvcom.406.305.suffix/1647019464547.jpeg", "Samosa", 15.0 },
                    { 2, "Appetizer", " Ipsa laudantium molestias eos sapiente officiis modi at sunt excepturi expedita sint", "https://cdn.cnn.com/cnnnext/dam/assets/200811115525-04-best-polish-foods-super-169.jpg", "Paneer Tika", 13.99 },
                    { 3, "Dessert", " IPerspiciatis minima nesciunt dolorem! Officiis iure rerum voluptates a cumque velitquibusdam sed amet tempora.Sit laborum ab,eius fugit doloribus tenetur", "https://i.ytimg.com/vi/R8Y7NWC5jgM/maxresdefault.jpg", "Sweet Pie", 10.99 },
                    { 4, "Entree", "Ipsa laudantium molestias eos sapiente officiis modi at sunt excepturi expedita sint ? Sed quibusdam recusandae alias error harum maxime adipisci amet laborum.Perspiciatis minima nesciunt dolorem! Officiis iure rerum voluptates a cumque velit quibusdam sed amet tempora.Sit laborum ab eius fugit doloribus teetur fugiat, temporibus enim commodi iusto", "https://cdn.cnn.com/cnnnext/dam/assets/220719164934-01-inexpensive-food-healthy-stock-super-tease.jpeg", "Pav Bhaji", 15.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
