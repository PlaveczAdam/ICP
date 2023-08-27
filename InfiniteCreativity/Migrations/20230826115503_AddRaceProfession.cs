using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfiniteCreativity.Migrations
{
    /// <inheritdoc />
    public partial class AddRaceProfession : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BaseHealth",
                table: "Character");

            migrationBuilder.AddColumn<int>(
                name: "Profession",
                table: "Character",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Race",
                table: "Character",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Profession",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "Race",
                table: "Character");

            migrationBuilder.AddColumn<double>(
                name: "BaseHealth",
                table: "Character",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
