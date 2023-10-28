using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfiniteCreativity.Migrations
{
    /// <inheritdoc />
    public partial class AddBattleBattleParticipantToContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BattleParticipants_Battle_BattleId",
                table: "BattleParticipants");

            migrationBuilder.AddForeignKey(
                name: "FK_BattleParticipants_Battle_BattleId",
                table: "BattleParticipants",
                column: "BattleId",
                principalTable: "Battle",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BattleParticipants_Battle_BattleId",
                table: "BattleParticipants");

            migrationBuilder.AddForeignKey(
                name: "FK_BattleParticipants_Battle_BattleId",
                table: "BattleParticipants",
                column: "BattleId",
                principalTable: "Battle",
                principalColumn: "Id");
        }
    }
}
