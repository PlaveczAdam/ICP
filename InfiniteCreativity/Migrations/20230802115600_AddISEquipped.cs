using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfiniteCreativity.Migrations
{
    /// <inheritdoc />
    public partial class AddISEquipped : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsEquipped",
                table: "Item",
                type: "boolean",
                nullable: true);
                
            migrationBuilder.AddColumn<bool>(
                name: "Weapon_IsEquipped",
                table: "Item",
                type: "boolean",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEquipped",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "Weapon_IsEquipped",
                table: "Item");
        }
    }
}
