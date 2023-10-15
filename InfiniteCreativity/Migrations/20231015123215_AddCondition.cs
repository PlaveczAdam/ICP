using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfiniteCreativity.Migrations
{
    /// <inheritdoc />
    public partial class AddCondition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Condition",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Duration = table.Column<int>(type: "integer", nullable: false),
                    ConditionDamageMultiplier = table.Column<double>(type: "double precision", nullable: false),
                    BattleParticipantId = table.Column<Guid>(type: "uuid", nullable: false),
                    Discriminator = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Condition", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Condition_BattleParticipants_BattleParticipantId",
                        column: x => x.BattleParticipantId,
                        principalTable: "BattleParticipants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConditionBlueprint",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    ConditionType = table.Column<int>(type: "integer", nullable: false),
                    Duration = table.Column<int>(type: "integer", nullable: false),
                    SkillId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConditionBlueprint", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ConditionBlueprint_Skill_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skill",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ConditionBlueprint",
                columns: new[] { "ID", "ConditionType", "Duration", "SkillId" },
                values: new object[] { new Guid("c0aefcab-0958-469f-a331-ea1b0967b557"), 0, 10, new Guid("ea380bc9-ccf3-4f9f-ab09-f72cf0229465") });

            migrationBuilder.CreateIndex(
                name: "IX_Condition_BattleParticipantId",
                table: "Condition",
                column: "BattleParticipantId");

            migrationBuilder.CreateIndex(
                name: "IX_ConditionBlueprint_SkillId",
                table: "ConditionBlueprint",
                column: "SkillId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Condition");

            migrationBuilder.DropTable(
                name: "ConditionBlueprint");
        }
    }
}
