using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfiniteCreativity.Migrations
{
    /// <inheritdoc />
    public partial class AddTargetToSkills : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TargetType",
                table: "Skill",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("be29078b-1e09-4b15-8802-77a8e3c8fd09"),
                column: "TargetType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("ea380bc9-ccf3-4f9f-ab09-f72cf0229465"),
                column: "TargetType",
                value: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TargetType",
                table: "Skill");
        }
    }
}
