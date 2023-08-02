using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfiniteCreativity.Migrations
{
    /// <inheritdoc />
    public partial class ChangeIsEquippedToEquippedCount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEquipped",
                table: "Item");

            migrationBuilder.AddColumn<int>(
                name: "EquipCount",
                table: "Item",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EquipCount",
                table: "Item");

            migrationBuilder.AddColumn<bool>(
                name: "IsEquipped",
                table: "Item",
                type: "boolean",
                nullable: true);
        }
    }
}
