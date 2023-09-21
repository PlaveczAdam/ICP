using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfiniteCreativity.Migrations
{
    /// <inheritdoc />
    public partial class AddCascadeToHexTileDataObject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HexTiles_EntityBase_DetailEntityId",
                table: "HexTiles");

            migrationBuilder.DropIndex(
                name: "IX_HexTiles_DetailEntityId",
                table: "HexTiles");

            migrationBuilder.CreateIndex(
                name: "IX_HexTiles_DetailEntityId",
                table: "HexTiles",
                column: "DetailEntityId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_HexTiles_EntityBase_DetailEntityId",
                table: "HexTiles",
                column: "DetailEntityId",
                principalTable: "EntityBase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HexTiles_EntityBase_DetailEntityId",
                table: "HexTiles");

            migrationBuilder.DropIndex(
                name: "IX_HexTiles_DetailEntityId",
                table: "HexTiles");

            migrationBuilder.CreateIndex(
                name: "IX_HexTiles_DetailEntityId",
                table: "HexTiles",
                column: "DetailEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_HexTiles_EntityBase_DetailEntityId",
                table: "HexTiles",
                column: "DetailEntityId",
                principalTable: "EntityBase",
                principalColumn: "Id");
        }
    }
}
