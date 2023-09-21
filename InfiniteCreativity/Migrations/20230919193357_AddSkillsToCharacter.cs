using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfiniteCreativity.Migrations
{
    /// <inheritdoc />
    public partial class AddSkillsToCharacter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CharacterSkillHolder",
                columns: table => new
                {
                    CharactersId = table.Column<int>(type: "integer", nullable: false),
                    SkillsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterSkillHolder", x => new { x.CharactersId, x.SkillsId });
                    table.ForeignKey(
                        name: "FK_CharacterSkillHolder_Character_CharactersId",
                        column: x => x.CharactersId,
                        principalTable: "Character",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterSkillHolder_Item_SkillsId",
                        column: x => x.SkillsId,
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharacterSkillHolder_SkillsId",
                table: "CharacterSkillHolder",
                column: "SkillsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacterSkillHolder");
        }
    }
}
