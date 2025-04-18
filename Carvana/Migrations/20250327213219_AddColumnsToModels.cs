using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Carvana.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnsToModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "Models",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "VehicleType",
                table: "Models",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Brand",
                table: "Models");

            migrationBuilder.DropColumn(
                name: "VehicleType",
                table: "Models");
        }
    }
}
