using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfiniteCreativity.Migrations
{
    /// <inheritdoc />
    public partial class AddMinion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BattleParticipants_Character_CharacterId",
                table: "BattleParticipants");

            migrationBuilder.DropIndex(
                name: "IX_BattleParticipants_CharacterId",
                table: "BattleParticipants");

            migrationBuilder.AddColumn<Guid>(
                name: "MinionId",
                table: "BattleParticipants",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Minion",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CasterId = table.Column<Guid>(type: "uuid", nullable: false),
                    CurrentDuration = table.Column<int>(type: "integer", nullable: true),
                    Side = table.Column<int>(type: "integer", nullable: false),
                    CurrentHealth = table.Column<double>(type: "double precision", nullable: false),
                    Discriminator = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Minion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Minion_BattleParticipants_CasterId",
                        column: x => x.CasterId,
                        principalTable: "BattleParticipants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MinionBlueprint",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    SkillId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MinionBlueprint", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MinionBlueprint_Skill_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skill",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "MinionBlueprint",
                columns: new[] { "Id", "SkillId", "Type" },
                values: new object[] { new Guid("c46db327-c6c5-4ec2-b093-f5c7cc3bca07"), null, 0 });

            migrationBuilder.InsertData(
                table: "Skill",
                columns: new[] { "Id", "AbilityGaugeCost", "Cooldown", "Damage", "Description", "Name", "ResourceCost", "TargetType" },
                values: new object[] { new Guid("0d97f399-4303-43ab-af02-eab16fb5be04"), 2, 0, 0.0, "n", "BB", 1.0, 2 });

            migrationBuilder.CreateIndex(
                name: "IX_BattleParticipants_CharacterId",
                table: "BattleParticipants",
                column: "CharacterId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BattleParticipants_MinionId",
                table: "BattleParticipants",
                column: "MinionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Minion_CasterId",
                table: "Minion",
                column: "CasterId");

            migrationBuilder.CreateIndex(
                name: "IX_MinionBlueprint_SkillId",
                table: "MinionBlueprint",
                column: "SkillId");

            migrationBuilder.AddForeignKey(
                name: "FK_BattleParticipants_Character_CharacterId",
                table: "BattleParticipants",
                column: "CharacterId",
                principalTable: "Character",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BattleParticipants_Minion_MinionId",
                table: "BattleParticipants",
                column: "MinionId",
                principalTable: "Minion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BattleParticipants_Character_CharacterId",
                table: "BattleParticipants");

            migrationBuilder.DropForeignKey(
                name: "FK_BattleParticipants_Minion_MinionId",
                table: "BattleParticipants");

            migrationBuilder.DropTable(
                name: "Minion");

            migrationBuilder.DropTable(
                name: "MinionBlueprint");

            migrationBuilder.DropIndex(
                name: "IX_BattleParticipants_CharacterId",
                table: "BattleParticipants");

            migrationBuilder.DropIndex(
                name: "IX_BattleParticipants_MinionId",
                table: "BattleParticipants");

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("0d97f399-4303-43ab-af02-eab16fb5be04"));

            migrationBuilder.DropColumn(
                name: "MinionId",
                table: "BattleParticipants");

            migrationBuilder.CreateIndex(
                name: "IX_BattleParticipants_CharacterId",
                table: "BattleParticipants",
                column: "CharacterId");

            migrationBuilder.AddForeignKey(
                name: "FK_BattleParticipants_Character_CharacterId",
                table: "BattleParticipants",
                column: "CharacterId",
                principalTable: "Character",
                principalColumn: "Id");
        }
    }
}
