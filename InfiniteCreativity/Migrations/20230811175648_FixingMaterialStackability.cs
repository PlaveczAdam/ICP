using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfiniteCreativity.Migrations
{
    /// <inheritdoc />
    public partial class FixingMaterialStackability : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StackSize",
                table: "Item");

            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "Item",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StackableType",
                table: "Item",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "StackableType",
                table: "Item");

            migrationBuilder.AddColumn<int>(
                name: "StackSize",
                table: "Item",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
