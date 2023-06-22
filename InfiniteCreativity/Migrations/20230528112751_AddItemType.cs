using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace InfiniteCreativity.Migrations
{
    /// <inheritdoc />
    public partial class AddItemType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ItemType",
                table: "Item",
                type: "integer",
                nullable: false,
                defaultValue: 0
            );

            migrationBuilder.AddColumn<int>(
                name: "QuestId",
                table: "Item",
                type: "integer",
                nullable: true
            );

            migrationBuilder.AddColumn<double>(
                name: "Range",
                table: "Item",
                type: "double precision",
                nullable: true
            );

            migrationBuilder.AddColumn<double>(
                name: "Ranged_AttackSpeed",
                table: "Item",
                type: "double precision",
                nullable: true
            );

            migrationBuilder.AddColumn<double>(
                name: "Ranged_CritChance",
                table: "Item",
                type: "double precision",
                nullable: true
            );

            migrationBuilder.AddColumn<double>(
                name: "Ranged_CritMultiplier",
                table: "Item",
                type: "double precision",
                nullable: true
            );

            migrationBuilder.AddColumn<double>(
                name: "Ranged_Damage",
                table: "Item",
                type: "double precision",
                nullable: true
            );

            migrationBuilder.AddColumn<double>(
                name: "Reload",
                table: "Item",
                type: "double precision",
                nullable: true
            );

            migrationBuilder.CreateTable(
                name: "Quest",
                columns: table =>
                    new
                    {
                        Id = table
                            .Column<int>(type: "integer", nullable: false)
                            .Annotation(
                                "Npgsql:ValueGenerationStrategy",
                                NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                            ),
                        Name = table.Column<string>(type: "text", nullable: false),
                        Description = table.Column<string>(type: "text", nullable: false),
                        Progression = table.Column<double>(
                            type: "double precision",
                            nullable: false
                        ),
                        CharacterId = table.Column<int>(type: "integer", nullable: true)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Quest_Character_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Character",
                        principalColumn: "Id"
                    );
                }
            );

            migrationBuilder.CreateIndex(name: "IX_Item_QuestId", table: "Item", column: "QuestId");

            migrationBuilder.CreateIndex(
                name: "IX_Quest_CharacterId",
                table: "Quest",
                column: "CharacterId"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Item_Quest_QuestId",
                table: "Item",
                column: "QuestId",
                principalTable: "Quest",
                principalColumn: "Id"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Item_Quest_QuestId", table: "Item");

            migrationBuilder.DropTable(name: "Quest");

            migrationBuilder.DropIndex(name: "IX_Item_QuestId", table: "Item");

            migrationBuilder.DropColumn(name: "ItemType", table: "Item");

            migrationBuilder.DropColumn(name: "QuestId", table: "Item");

            migrationBuilder.DropColumn(name: "Range", table: "Item");

            migrationBuilder.DropColumn(name: "Ranged_AttackSpeed", table: "Item");

            migrationBuilder.DropColumn(name: "Ranged_CritChance", table: "Item");

            migrationBuilder.DropColumn(name: "Ranged_CritMultiplier", table: "Item");

            migrationBuilder.DropColumn(name: "Ranged_Damage", table: "Item");

            migrationBuilder.DropColumn(name: "Reload", table: "Item");
        }
    }
}
