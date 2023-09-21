using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfiniteCreativity.Migrations
{
    /// <inheritdoc />
    public partial class ReverseRelationForEntityBase : Migration
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

            migrationBuilder.DropColumn(
                name: "DetailEntityId",
                table: "HexTiles");

            migrationBuilder.AddColumn<Guid>(
                name: "HexTileDataObjectId",
                table: "EntityBase",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_HexTiles_Id",
                table: "HexTiles",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EntityBase_HexTileDataObjectId",
                table: "EntityBase",
                column: "HexTileDataObjectId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EntityBase_HexTiles_HexTileDataObjectId",
                table: "EntityBase",
                column: "HexTileDataObjectId",
                principalTable: "HexTiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EntityBase_HexTiles_HexTileDataObjectId",
                table: "EntityBase");

            migrationBuilder.DropIndex(
                name: "IX_HexTiles_Id",
                table: "HexTiles");

            migrationBuilder.DropIndex(
                name: "IX_EntityBase_HexTileDataObjectId",
                table: "EntityBase");

            migrationBuilder.DropColumn(
                name: "HexTileDataObjectId",
                table: "EntityBase");

            migrationBuilder.AddColumn<int>(
                name: "DetailEntityId",
                table: "HexTiles",
                type: "integer",
                nullable: true);

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
    }
}
