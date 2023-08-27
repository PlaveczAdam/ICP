using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfiniteCreativity.Migrations
{
    /// <inheritdoc />
    public partial class SolveForeignkeyProblemOnEntityBaseDataObject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HexTiles_EntityBase_DetailEntityId",
                table: "HexTiles");

            migrationBuilder.AlterColumn<int>(
                name: "DetailEntityId",
                table: "HexTiles",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_HexTiles_EntityBase_DetailEntityId",
                table: "HexTiles",
                column: "DetailEntityId",
                principalTable: "EntityBase",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HexTiles_EntityBase_DetailEntityId",
                table: "HexTiles");

            migrationBuilder.AlterColumn<int>(
                name: "DetailEntityId",
                table: "HexTiles",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_HexTiles_EntityBase_DetailEntityId",
                table: "HexTiles",
                column: "DetailEntityId",
                principalTable: "EntityBase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
