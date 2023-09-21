using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfiniteCreativity.Migrations
{
    /// <inheritdoc />
    public partial class CharacterSkillSLots : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacterSkillHolder");

            migrationBuilder.CreateTable(
                name: "CharacterSkillSlot",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CharacterId = table.Column<int>(type: "integer", nullable: false),
                    SkillHolderId = table.Column<Guid>(type: "uuid", nullable: true),
                    SlotNumber = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterSkillSlot", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CharacterSkillSlot_Character_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Character",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterSkillSlot_Item_SkillHolderId",
                        column: x => x.SkillHolderId,
                        principalTable: "Item",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharacterSkillSlot_CharacterId",
                table: "CharacterSkillSlot",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterSkillSlot_SkillHolderId",
                table: "CharacterSkillSlot",
                column: "SkillHolderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacterSkillSlot");

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
    }
}
