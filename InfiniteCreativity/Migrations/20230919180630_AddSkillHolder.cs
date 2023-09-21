using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfiniteCreativity.Migrations
{
    /// <inheritdoc />
    public partial class AddSkillHolder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SkillId",
                table: "Item",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Item_SkillId",
                table: "Item",
                column: "SkillId");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_Skill_SkillId",
                table: "Item",
                column: "SkillId",
                principalTable: "Skill",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_Skill_SkillId",
                table: "Item");

            migrationBuilder.DropIndex(
                name: "IX_Item_SkillId",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "SkillId",
                table: "Item");
        }
    }
}
