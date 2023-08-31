using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfiniteCreativity.Migrations
{
    /// <inheritdoc />
    public partial class AddPlayerToGConnection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GConnection_Player_PlayerId",
                table: "GConnection");

            migrationBuilder.AlterColumn<int>(
                name: "PlayerId",
                table: "GConnection",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GConnection_Player_PlayerId",
                table: "GConnection",
                column: "PlayerId",
                principalTable: "Player",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GConnection_Player_PlayerId",
                table: "GConnection");

            migrationBuilder.AlterColumn<int>(
                name: "PlayerId",
                table: "GConnection",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_GConnection_Player_PlayerId",
                table: "GConnection",
                column: "PlayerId",
                principalTable: "Player",
                principalColumn: "Id");
        }
    }
}
