using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieStoreRentalService.Migrations
{
    public partial class AddedRentalDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Rentals",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Rentals");
        }
    }
}
