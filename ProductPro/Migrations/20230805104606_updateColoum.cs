using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductPro.Migrations
{
    /// <inheritdoc />
    public partial class updateColoum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Details",
                table: "Products",
                newName: "Detail");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Detail",
                table: "Products",
                newName: "Details");
        }
    }
}
