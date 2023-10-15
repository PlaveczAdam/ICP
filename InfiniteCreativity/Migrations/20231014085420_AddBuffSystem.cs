using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfiniteCreativity.Migrations
{
    /// <inheritdoc />
    public partial class AddBuffSystem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BattleParticipantBuff");

            migrationBuilder.AddColumn<Guid>(
                name: "BattleParticipantId",
                table: "Buff",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Buff_BattleParticipantId",
                table: "Buff",
                column: "BattleParticipantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Buff_BattleParticipants_BattleParticipantId",
                table: "Buff",
                column: "BattleParticipantId",
                principalTable: "BattleParticipants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buff_BattleParticipants_BattleParticipantId",
                table: "Buff");

            migrationBuilder.DropIndex(
                name: "IX_Buff_BattleParticipantId",
                table: "Buff");

            migrationBuilder.DropColumn(
                name: "BattleParticipantId",
                table: "Buff");

            migrationBuilder.CreateTable(
                name: "BattleParticipantBuff",
                columns: table => new
                {
                    BattleParticipantsId = table.Column<Guid>(type: "uuid", nullable: false),
                    BuffsID = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BattleParticipantBuff", x => new { x.BattleParticipantsId, x.BuffsID });
                    table.ForeignKey(
                        name: "FK_BattleParticipantBuff_BattleParticipants_BattleParticipants~",
                        column: x => x.BattleParticipantsId,
                        principalTable: "BattleParticipants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BattleParticipantBuff_Buff_BuffsID",
                        column: x => x.BuffsID,
                        principalTable: "Buff",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BattleParticipantBuff_BuffsID",
                table: "BattleParticipantBuff",
                column: "BuffsID");
        }
    }
}
