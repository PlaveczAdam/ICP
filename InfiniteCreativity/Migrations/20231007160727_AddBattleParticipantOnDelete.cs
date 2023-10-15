using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfiniteCreativity.Migrations
{
    /// <inheritdoc />
    public partial class AddBattleParticipantOnDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BattleParticipants_Enemy_EnemyId",
                table: "BattleParticipants");

            migrationBuilder.AddForeignKey(
                name: "FK_BattleParticipants_Enemy_EnemyId",
                table: "BattleParticipants",
                column: "EnemyId",
                principalTable: "Enemy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BattleParticipants_Enemy_EnemyId",
                table: "BattleParticipants");

            migrationBuilder.AddForeignKey(
                name: "FK_BattleParticipants_Enemy_EnemyId",
                table: "BattleParticipants",
                column: "EnemyId",
                principalTable: "Enemy",
                principalColumn: "Id");
        }
    }
}
