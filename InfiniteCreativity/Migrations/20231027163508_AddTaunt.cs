using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfiniteCreativity.Migrations
{
    /// <inheritdoc />
    public partial class AddTaunt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CasterId",
                table: "Condition",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CasterId",
                table: "Buff",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "Skill",
                columns: new[] { "Id", "AbilityGaugeCost", "Cooldown", "Damage", "Description", "Name", "ResourceCost", "TargetType" },
                values: new object[] { new Guid("c4c4ed52-a0bf-449a-80ec-a9f38338a695"), 1, 1, 0.0, "n", "Taunt", 1.0, 0 });

            migrationBuilder.InsertData(
                table: "ConditionBlueprint",
                columns: new[] { "ID", "ConditionType", "Duration", "SkillId", "Stacks" },
                values: new object[] { new Guid("6cbfcda0-7914-4891-959c-4536eb568137"), 2, 20, new Guid("c4c4ed52-a0bf-449a-80ec-a9f38338a695"), 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Condition_CasterId",
                table: "Condition",
                column: "CasterId");

            migrationBuilder.CreateIndex(
                name: "IX_Buff_CasterId",
                table: "Buff",
                column: "CasterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Buff_BattleParticipants_CasterId",
                table: "Buff",
                column: "CasterId",
                principalTable: "BattleParticipants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Condition_BattleParticipants_CasterId",
                table: "Condition",
                column: "CasterId",
                principalTable: "BattleParticipants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buff_BattleParticipants_CasterId",
                table: "Buff");

            migrationBuilder.DropForeignKey(
                name: "FK_Condition_BattleParticipants_CasterId",
                table: "Condition");

            migrationBuilder.DropIndex(
                name: "IX_Condition_CasterId",
                table: "Condition");

            migrationBuilder.DropIndex(
                name: "IX_Buff_CasterId",
                table: "Buff");

            migrationBuilder.DeleteData(
                table: "ConditionBlueprint",
                keyColumn: "ID",
                keyValue: new Guid("6cbfcda0-7914-4891-959c-4536eb568137"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("c4c4ed52-a0bf-449a-80ec-a9f38338a695"));

            migrationBuilder.DropColumn(
                name: "CasterId",
                table: "Condition");

            migrationBuilder.DropColumn(
                name: "CasterId",
                table: "Buff");
        }
    }
}
