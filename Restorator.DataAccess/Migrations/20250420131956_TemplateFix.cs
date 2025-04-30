using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restorator.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class TemplateFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "RestaurantTemplates",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "RestaurantTemplates");
        }
    }
}
