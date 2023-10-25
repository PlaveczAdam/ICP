using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfiniteCreativity.Migrations
{
    /// <inheritdoc />
    public partial class FixCharacterSkillSLotDeletion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CharacterSkillSlot_Item_SkillHolderId",
                table: "CharacterSkillSlot");

            migrationBuilder.AddForeignKey(
                name: "FK_CharacterSkillSlot_Item_SkillHolderId",
                table: "CharacterSkillSlot",
                column: "SkillHolderId",
                principalTable: "Item",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CharacterSkillSlot_Item_SkillHolderId",
                table: "CharacterSkillSlot");

            migrationBuilder.AddForeignKey(
                name: "FK_CharacterSkillSlot_Item_SkillHolderId",
                table: "CharacterSkillSlot",
                column: "SkillHolderId",
                principalTable: "Item",
                principalColumn: "Id");
        }
    }
}
