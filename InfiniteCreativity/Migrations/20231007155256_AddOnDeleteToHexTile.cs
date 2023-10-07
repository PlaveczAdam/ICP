using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfiniteCreativity.Migrations
{
    /// <inheritdoc />
    public partial class AddOnDeleteToHexTile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HexTiles_Enemy_EnemyId",
                table: "HexTiles");

            migrationBuilder.AddForeignKey(
                name: "FK_HexTiles_Enemy_EnemyId",
                table: "HexTiles",
                column: "EnemyId",
                principalTable: "Enemy",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HexTiles_Enemy_EnemyId",
                table: "HexTiles");

            migrationBuilder.AddForeignKey(
                name: "FK_HexTiles_Enemy_EnemyId",
                table: "HexTiles",
                column: "EnemyId",
                principalTable: "Enemy",
                principalColumn: "Id");
        }
    }
}
