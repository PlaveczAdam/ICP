using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfiniteCreativity.Migrations
{
    /// <inheritdoc />
    public partial class ChangePlayerPurseGlobal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Purse",
                table: "Player",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.Sql(@"
                CREATE TEMP TABLE temptable AS
                SELECT ""PlayerId"", SUM(""Purse"") AS sumpurse
                FROM ""Character""
                GROUP BY ""PlayerId"";
                
                UPDATE ""Player""
                SET ""Purse"" = ""temptable"".""sumpurse""
                FROM ""temptable""
                WHERE ""temptable"".""PlayerId""=""Player"".""Id"";
                ");

            migrationBuilder.DropColumn(
                name: "Purse",
                table: "Character"); 
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Purse",
                table: "Player");

            migrationBuilder.AddColumn<int>(
                name: "Purse",
                table: "Character",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
