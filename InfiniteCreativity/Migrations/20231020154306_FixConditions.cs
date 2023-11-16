using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfiniteCreativity.Migrations
{
    /// <inheritdoc />
    public partial class FixConditions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Stacks",
                table: "ConditionBlueprint",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "ConditionBlueprint",
                keyColumn: "ID",
                keyValue: new Guid("316ec03b-c2cd-4196-aaef-e3fa0c203d6d"),
                column: "Stacks",
                value: 1);

            migrationBuilder.UpdateData(
                table: "ConditionBlueprint",
                keyColumn: "ID",
                keyValue: new Guid("c0aefcab-0958-469f-a331-ea1b0967b557"),
                column: "Stacks",
                value: 1);

            migrationBuilder.InsertData(
                table: "Skill",
                columns: new[] { "Id", "AbilityGaugeCost", "Cooldown", "Damage", "Description", "Name", "ResourceCost", "TargetType" },
                values: new object[] { new Guid("3464d035-cacd-44bb-ade9-da5a1ca2b0d9"), 1, 1, 0.5, "n", "BigBleed", 1.0, 0 });

            migrationBuilder.InsertData(
                table: "ConditionBlueprint",
                columns: new[] { "ID", "ConditionType", "Duration", "SkillId", "Stacks" },
                values: new object[] { new Guid("2a57c132-fda6-4c02-85a5-d774b4d8555d"), 0, 10, new Guid("3464d035-cacd-44bb-ade9-da5a1ca2b0d9"), 99 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ConditionBlueprint",
                keyColumn: "ID",
                keyValue: new Guid("2a57c132-fda6-4c02-85a5-d774b4d8555d"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("3464d035-cacd-44bb-ade9-da5a1ca2b0d9"));

            migrationBuilder.DropColumn(
                name: "Stacks",
                table: "ConditionBlueprint");
        }
    }
}
