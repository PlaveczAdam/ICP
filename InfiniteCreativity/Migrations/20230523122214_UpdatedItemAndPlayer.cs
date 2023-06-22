using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfiniteCreativity.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedItemAndPlayer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Player_Character_CharacterId",
                table: "Player"
            );

            migrationBuilder.DropIndex(name: "IX_Player_CharacterId", table: "Player");

            migrationBuilder.DropColumn(name: "CharacterId", table: "Player");

            migrationBuilder.AddColumn<int>(
                name: "PlayerId",
                table: "Character",
                type: "integer",
                nullable: true
            );

            migrationBuilder.CreateIndex(
                name: "IX_Character_PlayerId",
                table: "Character",
                column: "PlayerId"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Character_Player_PlayerId",
                table: "Character",
                column: "PlayerId",
                principalTable: "Player",
                principalColumn: "Id"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Character_Player_PlayerId",
                table: "Character"
            );

            migrationBuilder.DropIndex(name: "IX_Character_PlayerId", table: "Character");

            migrationBuilder.DropColumn(name: "PlayerId", table: "Character");

            migrationBuilder.AddColumn<int>(
                name: "CharacterId",
                table: "Player",
                type: "integer",
                nullable: false,
                defaultValue: 0
            );

            migrationBuilder.CreateIndex(
                name: "IX_Player_CharacterId",
                table: "Player",
                column: "CharacterId"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Player_Character_CharacterId",
                table: "Player",
                column: "CharacterId",
                principalTable: "Character",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );
        }
    }
}
