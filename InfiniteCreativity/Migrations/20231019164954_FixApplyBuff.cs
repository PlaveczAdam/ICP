using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfiniteCreativity.Migrations
{
    /// <inheritdoc />
    public partial class FixApplyBuff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Stacks",
                table: "BuffBlueprint",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "BuffBlueprint",
                keyColumn: "ID",
                keyValue: new Guid("96660f57-f437-4e15-a469-7f596c6ccccc"),
                column: "Stacks",
                value: 1);

            migrationBuilder.UpdateData(
                table: "BuffBlueprint",
                keyColumn: "ID",
                keyValue: new Guid("b4b2e548-24f8-4a86-8308-b634284fb0e8"),
                column: "Stacks",
                value: 99);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Stacks",
                table: "BuffBlueprint");
        }
    }
}
