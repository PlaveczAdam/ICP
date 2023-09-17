using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace InfiniteCreativity.Migrations
{
    /// <inheritdoc />
    public partial class AddStatsAndEnemy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Turn",
                table: "GConnection",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "CurrentHealth",
                table: "Character",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "Enemy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Level = table.Column<double>(type: "double precision", nullable: false),
                    EnemyType = table.Column<int>(type: "integer", nullable: false),
                    Health = table.Column<double>(type: "double precision", nullable: false),
                    GConnectionConnectionID = table.Column<string>(type: "text", nullable: true),
                    Discriminator = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enemy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Enemy_GConnection_GConnectionConnectionID",
                        column: x => x.GConnectionConnectionID,
                        principalTable: "GConnection",
                        principalColumn: "ConnectionID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Enemy_GConnectionConnectionID",
                table: "Enemy",
                column: "GConnectionConnectionID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Enemy");

            migrationBuilder.DropColumn(
                name: "Turn",
                table: "GConnection");

            migrationBuilder.DropColumn(
                name: "CurrentHealth",
                table: "Character");
        }
    }
}
