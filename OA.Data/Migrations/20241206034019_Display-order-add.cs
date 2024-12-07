using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OA.Data.Migrations
{
    /// <inheritdoc />
    public partial class Displayorderadd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DisplayOrder",
                table: "State",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DisplayOrder",
                table: "City",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DisplayOrder",
                table: "Area",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayOrder",
                table: "State");

            migrationBuilder.DropColumn(
                name: "DisplayOrder",
                table: "City");

            migrationBuilder.DropColumn(
                name: "DisplayOrder",
                table: "Area");
        }
    }
}
