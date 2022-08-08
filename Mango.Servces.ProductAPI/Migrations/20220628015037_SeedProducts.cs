using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Mango.Servces.ProductAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Products",
                newName: "ProductId");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryName", "Description", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Appetizer", "Lorem ipsum dolor sit amet consectetur adipisicing elit. Maxime mollitia, eaque rerum! Provident si", "https://dotnetmastery312.blob.core.windows.net/mango/1.jfif", "Samosa", 15.0 },
                    { 2, "Appetizer", " Ipsa laudantium molestias eos sapiente officiis modi at sunt excepturi expedita sint", "https://dotnetmastery312.blob.core.windows.net/mango/2.jfif", "Paneer Tika", 13.99 },
                    { 3, "Dessert", " IPerspiciatis minima nesciunt dolorem! Officiis iure rerum voluptates a cumque velitquibusdam sed amet tempora.Sit laborum ab,eius fugit doloribus tenetur", "https://dotnetmastery312.blob.core.windows.net/mango/3.jfif", "Sweet Pie", 10.99 },
                    { 4, "Entree", "Ipsa laudantium molestias eos sapiente officiis modi at sunt excepturi expedita sint ? Sed quibusdam recusandae alias error harum maxime adipisci amet laborum.Perspiciatis minima nesciunt dolorem! Officiis iure rerum voluptates a cumque velit quibusdam sed amet tempora.Sit laborum ab eius fugit doloribus teetur fugiat, temporibus enim commodi iusto", "https://dotnetmastery312.blob.core.windows.net/mango/4.jfif", "Pav Bhaji", 15.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4);

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Products",
                newName: "ProductId");
        }
    }
}
