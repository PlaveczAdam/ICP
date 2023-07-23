using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfiniteCreativity.Migrations
{
    /// <inheritdoc />
    public partial class PlayerwideInventory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_Character_CharacterId",
                table: "Item");

            migrationBuilder.RenameColumn(
                name: "CharacterId",
                table: "Item",
                newName: "PlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_Item_CharacterId",
                table: "Item",
                newName: "IX_Item_PlayerId");

            migrationBuilder.Sql(@"
                UPDATE ""Item""
                SET ""PlayerId""=""Character"".""PlayerId""
                FROM ""Character""
                WHERE ""Item"".""PlayerId"" = ""Character"".""Id"";
                ");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_Player_PlayerId",
                table: "Item",
                column: "PlayerId",
                principalTable: "Player",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            throw new InvalidOperationException("Character links cannot be restored");
            migrationBuilder.DropForeignKey(
                name: "FK_Item_Player_PlayerId",
                table: "Item");

            migrationBuilder.RenameColumn(
                name: "PlayerId",
                table: "Item",
                newName: "CharacterId");

            migrationBuilder.RenameIndex(
                name: "IX_Item_PlayerId",
                table: "Item",
                newName: "IX_Item_CharacterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_Character_CharacterId",
                table: "Item",
                column: "CharacterId",
                principalTable: "Character",
                principalColumn: "Id");
        }
    }
}
