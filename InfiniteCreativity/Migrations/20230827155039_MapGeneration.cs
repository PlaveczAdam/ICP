using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace InfiniteCreativity.Migrations
{
    /// <inheritdoc />
    public partial class MapGeneration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Col",
                table: "Character",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Row",
                table: "Character",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EntityBase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityBase", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Map",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GConnectionConnectionID = table.Column<string>(type: "text", nullable: false),
                    Rows = table.Column<int>(type: "integer", nullable: false),
                    Columns = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Map", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Map_GConnection_GConnectionConnectionID",
                        column: x => x.GConnectionConnectionID,
                        principalTable: "GConnection",
                        principalColumn: "ConnectionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HexTiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MapDataObjectId = table.Column<int>(type: "integer", nullable: false),
                    RowIdx = table.Column<int>(type: "integer", nullable: false),
                    ColIdx = table.Column<int>(type: "integer", nullable: false),
                    TileContent = table.Column<int>(type: "integer", nullable: false),
                    IsDiscovered = table.Column<bool>(type: "boolean", nullable: false),
                    ReservedForPath = table.Column<bool>(type: "boolean", nullable: false),
                    DetailEntityId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HexTiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HexTiles_EntityBase_DetailEntityId",
                        column: x => x.DetailEntityId,
                        principalTable: "EntityBase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HexTiles_Map_MapDataObjectId",
                        column: x => x.MapDataObjectId,
                        principalTable: "Map",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HexTiles_DetailEntityId",
                table: "HexTiles",
                column: "DetailEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_HexTiles_MapDataObjectId",
                table: "HexTiles",
                column: "MapDataObjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Map_GConnectionConnectionID",
                table: "Map",
                column: "GConnectionConnectionID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HexTiles");

            migrationBuilder.DropTable(
                name: "EntityBase");

            migrationBuilder.DropTable(
                name: "Map");

            migrationBuilder.DropColumn(
                name: "Col",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "Row",
                table: "Character");
        }
    }
}
