using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InfiniteCreativity.Migrations
{
    /// <inheritdoc />
    public partial class AddSkills : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "BuffBlueprint",
                columns: new[] { "ID", "BuffType", "Duration", "SkillId", "Stacks" },
                values: new object[,]
                {
                    { new Guid("3960c868-e5df-4d13-a91e-7bce5fde63c7"), 2, 10, new Guid("be29078b-1e09-4b15-8802-77a8e3c8fd09"), 1 },
                    { new Guid("c3b09419-556e-4d30-bca9-9718bdcbc333"), 3, 10, new Guid("be29078b-1e09-4b15-8802-77a8e3c8fd09"), 1 },
                    { new Guid("d6b517b1-5879-4cd0-ac10-d3e6486b1052"), 1, 10, new Guid("be29078b-1e09-4b15-8802-77a8e3c8fd09"), 420 }
                });

            migrationBuilder.InsertData(
                table: "Skill",
                columns: new[] { "Id", "AbilityGaugeCost", "Cooldown", "Damage", "Description", "Name", "ResourceCost", "TargetType" },
                values: new object[] { new Guid("b17e04d7-5c6f-4465-a38f-3efe1aacf724"), 1, 1, 0.0, "n", "BigProtection", 1.0, 1 });

            migrationBuilder.InsertData(
                table: "BuffBlueprint",
                columns: new[] { "ID", "BuffType", "Duration", "SkillId", "Stacks" },
                values: new object[] { new Guid("cb84ca00-0830-42a9-8680-058ab80d3a3c"), 3, 10, new Guid("b17e04d7-5c6f-4465-a38f-3efe1aacf724"), 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BuffBlueprint",
                keyColumn: "ID",
                keyValue: new Guid("3960c868-e5df-4d13-a91e-7bce5fde63c7"));

            migrationBuilder.DeleteData(
                table: "BuffBlueprint",
                keyColumn: "ID",
                keyValue: new Guid("c3b09419-556e-4d30-bca9-9718bdcbc333"));

            migrationBuilder.DeleteData(
                table: "BuffBlueprint",
                keyColumn: "ID",
                keyValue: new Guid("cb84ca00-0830-42a9-8680-058ab80d3a3c"));

            migrationBuilder.DeleteData(
                table: "BuffBlueprint",
                keyColumn: "ID",
                keyValue: new Guid("d6b517b1-5879-4cd0-ac10-d3e6486b1052"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("b17e04d7-5c6f-4465-a38f-3efe1aacf724"));
        }
    }
}
