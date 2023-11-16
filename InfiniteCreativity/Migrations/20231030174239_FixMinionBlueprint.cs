using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfiniteCreativity.Migrations
{
    /// <inheritdoc />
    public partial class FixMinionBlueprint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MinionBlueprint_Skill_SkillId",
                table: "MinionBlueprint");

            migrationBuilder.AlterColumn<Guid>(
                name: "SkillId",
                table: "MinionBlueprint",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "MinionBlueprint",
                keyColumn: "Id",
                keyValue: new Guid("c46db327-c6c5-4ec2-b093-f5c7cc3bca07"),
                column: "SkillId",
                value: new Guid("0d97f399-4303-43ab-af02-eab16fb5be04"));

            migrationBuilder.AddForeignKey(
                name: "FK_MinionBlueprint_Skill_SkillId",
                table: "MinionBlueprint",
                column: "SkillId",
                principalTable: "Skill",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MinionBlueprint_Skill_SkillId",
                table: "MinionBlueprint");

            migrationBuilder.AlterColumn<Guid>(
                name: "SkillId",
                table: "MinionBlueprint",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.UpdateData(
                table: "MinionBlueprint",
                keyColumn: "Id",
                keyValue: new Guid("c46db327-c6c5-4ec2-b093-f5c7cc3bca07"),
                column: "SkillId",
                value: null);

            migrationBuilder.AddForeignKey(
                name: "FK_MinionBlueprint_Skill_SkillId",
                table: "MinionBlueprint",
                column: "SkillId",
                principalTable: "Skill",
                principalColumn: "Id");
        }
    }
}
