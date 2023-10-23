using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InfiniteCreativity.Migrations
{
    /// <inheritdoc />
    public partial class AddCooldown : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrentCooldown",
                table: "CharacterSkillSlot",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Skill",
                columns: new[] { "Id", "AbilityGaugeCost", "Cooldown", "Damage", "Description", "Name", "ResourceCost", "TargetType" },
                values: new object[,]
                {
                    { new Guid("0dd69a53-d1fd-4d80-8add-af15ac0666a6"), 2, 0, 2.0, "nincs", "Debuff", 1.0, 0 },
                    { new Guid("1cefb293-b21c-415c-a2f9-a8b74104624e"), 1, 2, 0.0, "buff", "Generic Continous Buff", 2.0, 1 }
                });

            migrationBuilder.InsertData(
                table: "BuffBlueprint",
                columns: new[] { "ID", "BuffType", "Duration", "SkillId" },
                values: new object[] { new Guid("b4b2e548-24f8-4a86-8308-b634284fb0e8"), 1, 10, new Guid("1cefb293-b21c-415c-a2f9-a8b74104624e") });

            migrationBuilder.InsertData(
                table: "ConditionBlueprint",
                columns: new[] { "ID", "ConditionType", "Duration", "SkillId" },
                values: new object[] { new Guid("316ec03b-c2cd-4196-aaef-e3fa0c203d6d"), 1, 10, new Guid("0dd69a53-d1fd-4d80-8add-af15ac0666a6") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BuffBlueprint",
                keyColumn: "ID",
                keyValue: new Guid("b4b2e548-24f8-4a86-8308-b634284fb0e8"));

            migrationBuilder.DeleteData(
                table: "ConditionBlueprint",
                keyColumn: "ID",
                keyValue: new Guid("316ec03b-c2cd-4196-aaef-e3fa0c203d6d"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("0dd69a53-d1fd-4d80-8add-af15ac0666a6"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("1cefb293-b21c-415c-a2f9-a8b74104624e"));

            migrationBuilder.DropColumn(
                name: "CurrentCooldown",
                table: "CharacterSkillSlot");
        }
    }
}
