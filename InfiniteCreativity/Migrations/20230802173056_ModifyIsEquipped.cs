using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfiniteCreativity.Migrations
{
    /// <inheritdoc />
    public partial class ModifyIsEquipped : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                UPDATE ""Item""
                SET ""IsEquipped""=""Item"".""Weapon_IsEquipped""
                WHERE ""Item"".""Weapon_IsEquipped"" != null;
                ");

            migrationBuilder.DropColumn(
                name: "Weapon_IsEquipped",
                table: "Item");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Weapon_IsEquipped",
                table: "Item",
                type: "boolean",
                nullable: true);
        }
    }
}
