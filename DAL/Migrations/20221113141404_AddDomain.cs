using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VkWallReader.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddDomain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Domain",
                table: "CountedWalls",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Domain",
                table: "CountedWalls");
        }
    }
}
