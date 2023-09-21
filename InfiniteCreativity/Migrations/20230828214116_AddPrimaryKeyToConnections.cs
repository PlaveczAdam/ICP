using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace InfiniteCreativity.Migrations
{
    /// <inheritdoc />
    public partial class AddPrimaryKeyToConnections : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enemy_GConnection_GConnectionConnectionID",
                table: "Enemy");

            migrationBuilder.DropForeignKey(
                name: "FK_GameCharacter_GConnection_ConnectionID",
                table: "GameCharacter");

            migrationBuilder.DropForeignKey(
                name: "FK_Map_GConnection_GConnectionConnectionID",
                table: "Map");

            migrationBuilder.DropIndex(
                name: "IX_Map_GConnectionConnectionID",
                table: "Map");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GConnection",
                table: "GConnection");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FeConnection",
                table: "FeConnection");

            migrationBuilder.DropIndex(
                name: "IX_Enemy_GConnectionConnectionID",
                table: "Enemy");

            migrationBuilder.DropColumn(
                name: "GConnectionConnectionID",
                table: "Map");

            migrationBuilder.DropColumn(
                name: "GConnectionConnectionID",
                table: "Enemy");

            migrationBuilder.RenameColumn(
                name: "ConnectionID",
                table: "GameCharacter",
                newName: "ConnectionId");

            migrationBuilder.RenameIndex(
                name: "IX_GameCharacter_ConnectionID",
                table: "GameCharacter",
                newName: "IX_GameCharacter_ConnectionId");

            migrationBuilder.AddColumn<int>(
                name: "GConnectionId",
                table: "Map",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "GConnection",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

           
            migrationBuilder.DropColumn("ConnectionId", "GameCharacter");
            migrationBuilder.AddColumn<int>(
                name: "ConnectionId",
                table: "GameCharacter",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "FeConnection",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "GConnectionId",
                table: "Enemy",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_GConnection",
                table: "GConnection",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FeConnection",
                table: "FeConnection",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Map_GConnectionId",
                table: "Map",
                column: "GConnectionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Enemy_GConnectionId",
                table: "Enemy",
                column: "GConnectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Enemy_GConnection_GConnectionId",
                table: "Enemy",
                column: "GConnectionId",
                principalTable: "GConnection",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameCharacter_GConnection_ConnectionId",
                table: "GameCharacter",
                column: "ConnectionId",
                principalTable: "GConnection",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Map_GConnection_GConnectionId",
                table: "Map",
                column: "GConnectionId",
                principalTable: "GConnection",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enemy_GConnection_GConnectionId",
                table: "Enemy");

            migrationBuilder.DropForeignKey(
                name: "FK_GameCharacter_GConnection_ConnectionId",
                table: "GameCharacter");

            migrationBuilder.DropForeignKey(
                name: "FK_Map_GConnection_GConnectionId",
                table: "Map");

            migrationBuilder.DropIndex(
                name: "IX_Map_GConnectionId",
                table: "Map");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GConnection",
                table: "GConnection");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FeConnection",
                table: "FeConnection");

            migrationBuilder.DropIndex(
                name: "IX_Enemy_GConnectionId",
                table: "Enemy");

            migrationBuilder.DropColumn(
                name: "GConnectionId",
                table: "Map");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "GConnection");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "FeConnection");

            migrationBuilder.DropColumn(
                name: "GConnectionId",
                table: "Enemy");

            migrationBuilder.RenameColumn(
                name: "ConnectionId",
                table: "GameCharacter",
                newName: "ConnectionID");

            migrationBuilder.RenameIndex(
                name: "IX_GameCharacter_ConnectionId",
                table: "GameCharacter",
                newName: "IX_GameCharacter_ConnectionID");

            migrationBuilder.AddColumn<string>(
                name: "GConnectionConnectionID",
                table: "Map",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "ConnectionID",
                table: "GameCharacter",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "GConnectionConnectionID",
                table: "Enemy",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_GConnection",
                table: "GConnection",
                column: "ConnectionID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FeConnection",
                table: "FeConnection",
                column: "ConnectionID");

            migrationBuilder.CreateIndex(
                name: "IX_Map_GConnectionConnectionID",
                table: "Map",
                column: "GConnectionConnectionID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Enemy_GConnectionConnectionID",
                table: "Enemy",
                column: "GConnectionConnectionID");

            migrationBuilder.AddForeignKey(
                name: "FK_Enemy_GConnection_GConnectionConnectionID",
                table: "Enemy",
                column: "GConnectionConnectionID",
                principalTable: "GConnection",
                principalColumn: "ConnectionID");

            migrationBuilder.AddForeignKey(
                name: "FK_GameCharacter_GConnection_ConnectionID",
                table: "GameCharacter",
                column: "ConnectionID",
                principalTable: "GConnection",
                principalColumn: "ConnectionID");

            migrationBuilder.AddForeignKey(
                name: "FK_Map_GConnection_GConnectionConnectionID",
                table: "Map",
                column: "GConnectionConnectionID",
                principalTable: "GConnection",
                principalColumn: "ConnectionID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
