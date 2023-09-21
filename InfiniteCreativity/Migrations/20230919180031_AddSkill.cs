using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfiniteCreativity.Migrations
{
    /// <inheritdoc />
    public partial class AddSkill : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Skill",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Damage = table.Column<double>(type: "double precision", nullable: false),
                    ResourceCost = table.Column<double>(type: "double precision", nullable: false),
                    Cooldown = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skill", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Skill",
                columns: new[] { "Id", "Cooldown", "Damage", "Description", "Name", "ResourceCost" },
                values: new object[] { new Guid("ea380bc9-ccf3-4f9f-ab09-f72cf0229465"), 0, 2.0, "nincs", "First", 1.0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Skill");
        }
    }
}
