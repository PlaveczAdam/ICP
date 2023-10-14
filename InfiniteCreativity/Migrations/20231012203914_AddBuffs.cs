using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfiniteCreativity.Migrations
{
    /// <inheritdoc />
    public partial class AddBuffs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Buff",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Duration = table.Column<int>(type: "integer", nullable: false),
                    Discriminator = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buff", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BuffBlueprint",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    BuffType = table.Column<int>(type: "integer", nullable: false),
                    Duration = table.Column<int>(type: "integer", nullable: false),
                    SkillId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuffBlueprint", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BuffBlueprint_Skill_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skill",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BattleParticipantBuff",
                columns: table => new
                {
                    BattleParticipantsId = table.Column<Guid>(type: "uuid", nullable: false),
                    BuffsID = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BattleParticipantBuff", x => new { x.BattleParticipantsId, x.BuffsID });
                    table.ForeignKey(
                        name: "FK_BattleParticipantBuff_BattleParticipants_BattleParticipants~",
                        column: x => x.BattleParticipantsId,
                        principalTable: "BattleParticipants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BattleParticipantBuff_Buff_BuffsID",
                        column: x => x.BuffsID,
                        principalTable: "Buff",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "BuffBlueprint",
                columns: new[] { "ID", "BuffType", "Duration", "SkillId" },
                values: new object[] { new Guid("96660f57-f437-4e15-a469-7f596c6ccccc"), 0, 10, new Guid("be29078b-1e09-4b15-8802-77a8e3c8fd09") });

            migrationBuilder.CreateIndex(
                name: "IX_BattleParticipantBuff_BuffsID",
                table: "BattleParticipantBuff",
                column: "BuffsID");

            migrationBuilder.CreateIndex(
                name: "IX_BuffBlueprint_SkillId",
                table: "BuffBlueprint",
                column: "SkillId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BattleParticipantBuff");

            migrationBuilder.DropTable(
                name: "BuffBlueprint");

            migrationBuilder.DropTable(
                name: "Buff");
        }
    }
}
