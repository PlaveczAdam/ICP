using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace InfiniteCreativity.Migrations
{
    /// <inheritdoc />
    public partial class ChangeIdTypeToGUID : Migration
    {
       
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey("FK_Listing_Item_ItemId", "Listing");
            migrationBuilder.DropForeignKey("FK_Character_Item_BootId", "Character");
            migrationBuilder.DropForeignKey("FK_Character_Item_ChestId", "Character");
            migrationBuilder.DropForeignKey("FK_Character_Item_HandId", "Character");
            migrationBuilder.DropForeignKey("FK_Character_Item_HeadId", "Character");
            migrationBuilder.DropForeignKey("FK_Character_Item_LegId", "Character");
            migrationBuilder.DropForeignKey("FK_Character_Item_ShoulderId", "Character");
            migrationBuilder.DropForeignKey("FK_Character_Item_WeaponId", "Character");
            migrationBuilder.DropPrimaryKey("PK_Item", "Item");
            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "Listing")
                ;
            migrationBuilder.AddColumn<Guid>(
                name: "ItemId",
                table: "Listing",
                type: "uuid",
                nullable: false)
                ;

            migrationBuilder.DropColumn(
                 name: "Id",
                table: "Item")
                ;
            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Item",
                type: "uuid",
                nullable: false)
                ;
            migrationBuilder.DropColumn(
                 name: "Id",
                table: "HexTiles")
                ;
            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "HexTiles",
                type: "uuid",
                nullable: false);
            migrationBuilder.DropColumn(
                 name: "WeaponId",
                table: "Character")
                ;
            migrationBuilder.AddColumn<Guid>(
                name: "WeaponId",
                table: "Character",
                type: "uuid",
                nullable: true);
            migrationBuilder.DropColumn(
                  name: "ShoulderId",
                table: "Character")
                ;
            migrationBuilder.AddColumn<Guid>(
                name: "ShoulderId",
                table: "Character",
                type: "uuid",
                nullable: true);
            migrationBuilder.DropColumn(
                   name: "LegId",
                table: "Character")
                ;
            migrationBuilder.AddColumn<Guid>(
                name: "LegId",
                table: "Character",
                type: "uuid",
                nullable: true);
            migrationBuilder.DropColumn(
                  name: "HeadId",
                table: "Character")
                ;
            migrationBuilder.AddColumn<Guid>(
                name: "HeadId",
                table: "Character",
                type: "uuid",
                nullable: true);
            migrationBuilder.DropColumn(
                  name: "HandId",
                table: "Character")
                ;
            migrationBuilder.AddColumn<Guid>(
                name: "HandId",
                table: "Character",
                type: "uuid",
                nullable: true);
            migrationBuilder.DropColumn(
                  name: "ChestId",
                table: "Character")
                ;
            migrationBuilder.AddColumn<Guid>(
                name: "ChestId",
                table: "Character",
                type: "uuid",
                nullable: true);
            migrationBuilder.DropColumn(
                  name: "BootId",
                table: "Character")
                ;
            migrationBuilder.AddColumn<Guid>(
                name: "BootId",
                table: "Character",
                type: "uuid",
                nullable: true);
            migrationBuilder.AddPrimaryKey("PK_Item", "Item", "Id");
            migrationBuilder.AddForeignKey("FK_Character_Item_BootId", "Character", "BootId", "Item");
            migrationBuilder.AddForeignKey("FK_Character_Item_ChestId", "Character", "ChestId", "Item");
            migrationBuilder.AddForeignKey("FK_Character_Item_HandId", "Character", "HandId", "Item");
            migrationBuilder.AddForeignKey("FK_Character_Item_HeadId", "Character", "HeadId", "Item");
            migrationBuilder.AddForeignKey("FK_Character_Item_LegId", "Character", "LegId", "Item");
            migrationBuilder.AddForeignKey("FK_Character_Item_ShoulderId", "Character", "ShoulderId", "Item");
            migrationBuilder.AddForeignKey("FK_Character_Item_WeaponId", "Character", "WeaponId", "Item");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            throw new System.NotImplementedException();
            migrationBuilder.AlterColumn<int>(
                name: "ItemId",
                table: "Listing",
                type: "integer",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Item",
                type: "integer",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "HexTiles",
                type: "integer",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "WeaponId",
                table: "Character",
                type: "integer",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ShoulderId",
                table: "Character",
                type: "integer",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LegId",
                table: "Character",
                type: "integer",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "HeadId",
                table: "Character",
                type: "integer",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "HandId",
                table: "Character",
                type: "integer",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ChestId",
                table: "Character",
                type: "integer",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BootId",
                table: "Character",
                type: "integer",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);
        }
    }
}
