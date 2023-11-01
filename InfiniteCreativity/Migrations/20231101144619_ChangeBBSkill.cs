using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfiniteCreativity.Migrations
{
    /// <inheritdoc />
    public partial class ChangeBBSkill : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("0d97f399-4303-43ab-af02-eab16fb5be04"),
                columns: new[] { "AbilityGaugeCost", "ResourceCost" },
                values: new object[] { 0, 0.0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("0d97f399-4303-43ab-af02-eab16fb5be04"),
                columns: new[] { "AbilityGaugeCost", "ResourceCost" },
                values: new object[] { 2, 1.0 });
        }
    }
}
