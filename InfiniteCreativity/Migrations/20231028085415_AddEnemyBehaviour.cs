using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfiniteCreativity.Migrations
{
    /// <inheritdoc />
    public partial class AddEnemyBehaviour : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BattleParticipants_Enemy_EnemyId",
                table: "BattleParticipants");

            migrationBuilder.DropIndex(
                name: "IX_BattleParticipants_EnemyId",
                table: "BattleParticipants");

            migrationBuilder.AddColumn<int>(
                name: "BehaviourType",
                table: "Enemy",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BattleParticipants_EnemyId",
                table: "BattleParticipants",
                column: "EnemyId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BattleParticipants_Enemy_EnemyId",
                table: "BattleParticipants",
                column: "EnemyId",
                principalTable: "Enemy",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BattleParticipants_Enemy_EnemyId",
                table: "BattleParticipants");

            migrationBuilder.DropIndex(
                name: "IX_BattleParticipants_EnemyId",
                table: "BattleParticipants");

            migrationBuilder.DropColumn(
                name: "BehaviourType",
                table: "Enemy");

            migrationBuilder.CreateIndex(
                name: "IX_BattleParticipants_EnemyId",
                table: "BattleParticipants",
                column: "EnemyId");

            migrationBuilder.AddForeignKey(
                name: "FK_BattleParticipants_Enemy_EnemyId",
                table: "BattleParticipants",
                column: "EnemyId",
                principalTable: "Enemy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
