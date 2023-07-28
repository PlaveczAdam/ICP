using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfiniteCreativity.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ArmorType",
                table: "Item",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "ArmorValue",
                table: "Item",
                type: "double precision",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArmorType",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "ArmorValue",
                table: "Item");
        }
    }
}
