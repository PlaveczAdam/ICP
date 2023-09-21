using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfiniteCreativity.Migrations
{
    /// <inheritdoc />
    public partial class RemoveAmountFromListing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Listing");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "Listing",
                type: "numeric(20,0)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
