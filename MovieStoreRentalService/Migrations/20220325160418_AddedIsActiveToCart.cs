using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieStoreRentalService.Migrations
{
    public partial class AddedIsActiveToCart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "ShoppingCarts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "ShoppingCarts");
        }
    }
}
