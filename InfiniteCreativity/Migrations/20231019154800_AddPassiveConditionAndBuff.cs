using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfiniteCreativity.Migrations
{
    /// <inheritdoc />
    public partial class AddPassiveConditionAndBuff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("0dd69a53-d1fd-4d80-8add-af15ac0666a6"),
                columns: new[] { "Damage", "Name" },
                values: new object[] { 0.0, "Weakness" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("0dd69a53-d1fd-4d80-8add-af15ac0666a6"),
                columns: new[] { "Damage", "Name" },
                values: new object[] { 2.0, "Debuff" });
        }
    }
}
