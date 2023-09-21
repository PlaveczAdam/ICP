using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfiniteCreativity.Migrations
{
    /// <inheritdoc />
    public partial class AddEnemyAndMovement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EnemyId",
                table: "HexTiles",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CurrentMovement",
                table: "Character",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_HexTiles_EnemyId",
                table: "HexTiles",
                column: "EnemyId");

            migrationBuilder.AddForeignKey(
                name: "FK_HexTiles_Enemy_EnemyId",
                table: "HexTiles",
                column: "EnemyId",
                principalTable: "Enemy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HexTiles_Enemy_EnemyId",
                table: "HexTiles");

            migrationBuilder.DropIndex(
                name: "IX_HexTiles_EnemyId",
                table: "HexTiles");

            migrationBuilder.DropColumn(
                name: "EnemyId",
                table: "HexTiles");

            migrationBuilder.DropColumn(
                name: "CurrentMovement",
                table: "Character");
        }
    }
}
