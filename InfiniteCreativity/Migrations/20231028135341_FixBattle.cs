using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfiniteCreativity.Migrations
{
    /// <inheritdoc />
    public partial class FixBattle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Battle_BattleParticipants_NextInTurnId",
                table: "Battle");

            migrationBuilder.DropIndex(
                name: "IX_Battle_NextInTurnId",
                table: "Battle");

            migrationBuilder.CreateIndex(
                name: "IX_Battle_NextInTurnId",
                table: "Battle",
                column: "NextInTurnId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Battle_BattleParticipants_NextInTurnId",
                table: "Battle",
                column: "NextInTurnId",
                principalTable: "BattleParticipants",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Battle_BattleParticipants_NextInTurnId",
                table: "Battle");

            migrationBuilder.DropIndex(
                name: "IX_Battle_NextInTurnId",
                table: "Battle");

            migrationBuilder.CreateIndex(
                name: "IX_Battle_NextInTurnId",
                table: "Battle",
                column: "NextInTurnId");

            migrationBuilder.AddForeignKey(
                name: "FK_Battle_BattleParticipants_NextInTurnId",
                table: "Battle",
                column: "NextInTurnId",
                principalTable: "BattleParticipants",
                principalColumn: "Id");
        }
    }
}
