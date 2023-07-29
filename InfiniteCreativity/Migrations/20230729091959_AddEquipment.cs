using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfiniteCreativity.Migrations
{
    /// <inheritdoc />
    public partial class AddEquipment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BootId",
                table: "Character",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ChestId",
                table: "Character",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HandId",
                table: "Character",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HeadId",
                table: "Character",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LegId",
                table: "Character",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ShoulderId",
                table: "Character",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WeaponId",
                table: "Character",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Character_BootId",
                table: "Character",
                column: "BootId");

            migrationBuilder.CreateIndex(
                name: "IX_Character_ChestId",
                table: "Character",
                column: "ChestId");

            migrationBuilder.CreateIndex(
                name: "IX_Character_HandId",
                table: "Character",
                column: "HandId");

            migrationBuilder.CreateIndex(
                name: "IX_Character_HeadId",
                table: "Character",
                column: "HeadId");

            migrationBuilder.CreateIndex(
                name: "IX_Character_LegId",
                table: "Character",
                column: "LegId");

            migrationBuilder.CreateIndex(
                name: "IX_Character_ShoulderId",
                table: "Character",
                column: "ShoulderId");

            migrationBuilder.CreateIndex(
                name: "IX_Character_WeaponId",
                table: "Character",
                column: "WeaponId");

            migrationBuilder.AddForeignKey(
                name: "FK_Character_Item_BootId",
                table: "Character",
                column: "BootId",
                principalTable: "Item",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Character_Item_ChestId",
                table: "Character",
                column: "ChestId",
                principalTable: "Item",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Character_Item_HandId",
                table: "Character",
                column: "HandId",
                principalTable: "Item",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Character_Item_HeadId",
                table: "Character",
                column: "HeadId",
                principalTable: "Item",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Character_Item_LegId",
                table: "Character",
                column: "LegId",
                principalTable: "Item",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Character_Item_ShoulderId",
                table: "Character",
                column: "ShoulderId",
                principalTable: "Item",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Character_Item_WeaponId",
                table: "Character",
                column: "WeaponId",
                principalTable: "Item",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Character_Item_BootId",
                table: "Character");

            migrationBuilder.DropForeignKey(
                name: "FK_Character_Item_ChestId",
                table: "Character");

            migrationBuilder.DropForeignKey(
                name: "FK_Character_Item_HandId",
                table: "Character");

            migrationBuilder.DropForeignKey(
                name: "FK_Character_Item_HeadId",
                table: "Character");

            migrationBuilder.DropForeignKey(
                name: "FK_Character_Item_LegId",
                table: "Character");

            migrationBuilder.DropForeignKey(
                name: "FK_Character_Item_ShoulderId",
                table: "Character");

            migrationBuilder.DropForeignKey(
                name: "FK_Character_Item_WeaponId",
                table: "Character");

            migrationBuilder.DropIndex(
                name: "IX_Character_BootId",
                table: "Character");

            migrationBuilder.DropIndex(
                name: "IX_Character_ChestId",
                table: "Character");

            migrationBuilder.DropIndex(
                name: "IX_Character_HandId",
                table: "Character");

            migrationBuilder.DropIndex(
                name: "IX_Character_HeadId",
                table: "Character");

            migrationBuilder.DropIndex(
                name: "IX_Character_LegId",
                table: "Character");

            migrationBuilder.DropIndex(
                name: "IX_Character_ShoulderId",
                table: "Character");

            migrationBuilder.DropIndex(
                name: "IX_Character_WeaponId",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "BootId",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "ChestId",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "HandId",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "HeadId",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "LegId",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "ShoulderId",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "WeaponId",
                table: "Character");
        }
    }
}
