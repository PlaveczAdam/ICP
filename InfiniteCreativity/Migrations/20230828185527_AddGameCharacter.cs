using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfiniteCreativity.Migrations
{
    /// <inheritdoc />
    public partial class AddGameCharacter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GameCharacter",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CharacterId = table.Column<int>(type: "integer", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    ConnectionID = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameCharacter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameCharacter_Character_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Character",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameCharacter_GConnection_ConnectionID",
                        column: x => x.ConnectionID,
                        principalTable: "GConnection",
                        principalColumn: "ConnectionID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameCharacter_CharacterId",
                table: "GameCharacter",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_GameCharacter_ConnectionID",
                table: "GameCharacter",
                column: "ConnectionID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameCharacter");
        }
    }
}
