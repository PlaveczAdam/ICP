using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfiniteCreativity.Migrations
{
    /// <inheritdoc />
    public partial class AddOneToOneMapToGConn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Map_GConnectionConnectionID",
                table: "Map");

            migrationBuilder.CreateIndex(
                name: "IX_Map_GConnectionConnectionID",
                table: "Map",
                column: "GConnectionConnectionID",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Map_GConnectionConnectionID",
                table: "Map");

            migrationBuilder.CreateIndex(
                name: "IX_Map_GConnectionConnectionID",
                table: "Map",
                column: "GConnectionConnectionID");
        }
    }
}
