using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restorator.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class DupFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Restaurants");

            migrationBuilder.AddColumn<string>(
                name: "Images",
                table: "Restaurants",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Images",
                table: "Restaurants");

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Restaurants",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
