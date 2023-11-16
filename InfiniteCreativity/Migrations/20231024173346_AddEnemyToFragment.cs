using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfiniteCreativity.Migrations
{
    /// <inheritdoc />
    public partial class AddEnemyToFragment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EnemyBlueprintId",
                table: "HexTiles",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EnemyBlueprint",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IsBoss = table.Column<bool>(type: "boolean", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnemyBlueprint", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HexTiles_EnemyBlueprintId",
                table: "HexTiles",
                column: "EnemyBlueprintId");

            migrationBuilder.AddForeignKey(
                name: "FK_HexTiles_EnemyBlueprint_EnemyBlueprintId",
                table: "HexTiles",
                column: "EnemyBlueprintId",
                principalTable: "EnemyBlueprint",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HexTiles_EnemyBlueprint_EnemyBlueprintId",
                table: "HexTiles");

            migrationBuilder.DropTable(
                name: "EnemyBlueprint");

            migrationBuilder.DropIndex(
                name: "IX_HexTiles_EnemyBlueprintId",
                table: "HexTiles");

            migrationBuilder.DropColumn(
                name: "EnemyBlueprintId",
                table: "HexTiles");
        }
    }
}
