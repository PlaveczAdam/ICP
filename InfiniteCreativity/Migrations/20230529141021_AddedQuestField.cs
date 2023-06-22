using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfiniteCreativity.Migrations
{
    /// <inheritdoc />
    public partial class AddedQuestField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quest_Character_CharacterId",
                table: "Quest");

            migrationBuilder.DropColumn(
                name: "Ranged_AttackSpeed",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "Ranged_CritChance",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "Ranged_CritMultiplier",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "Ranged_Damage",
                table: "Item");

            migrationBuilder.AlterColumn<int>(
                name: "CharacterId",
                table: "Quest",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WeaponType",
                table: "Item",
                type: "integer",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Quest_Character_CharacterId",
                table: "Quest",
                column: "CharacterId",
                principalTable: "Character",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quest_Character_CharacterId",
                table: "Quest");

            migrationBuilder.DropColumn(
                name: "WeaponType",
                table: "Item");

            migrationBuilder.AlterColumn<int>(
                name: "CharacterId",
                table: "Quest",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<double>(
                name: "Ranged_AttackSpeed",
                table: "Item",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Ranged_CritChance",
                table: "Item",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Ranged_CritMultiplier",
                table: "Item",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Ranged_Damage",
                table: "Item",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Quest_Character_CharacterId",
                table: "Quest",
                column: "CharacterId",
                principalTable: "Character",
                principalColumn: "Id");
        }
    }
}
