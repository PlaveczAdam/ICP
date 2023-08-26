using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfiniteCreativity.Migrations
{
    /// <inheritdoc />
    public partial class AddBasicStats : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Health",
                table: "Item",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Movement",
                table: "Item",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Health",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "Movement",
                table: "Item");
        }
    }
}
