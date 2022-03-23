using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieStoreRentalService.Migrations
{
    public partial class FixedShoppingCart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCarts_Rentals_RentalsId",
                table: "ShoppingCarts");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCarts_RentalsId",
                table: "ShoppingCarts");

            migrationBuilder.DropColumn(
                name: "RentalsId",
                table: "ShoppingCarts");

            migrationBuilder.CreateTable(
                name: "ShoppingCartsRentals",
                columns: table => new
                {
                    ShoppingCartsId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RentalsId = table.Column<string>(type: "nvarchar(70)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCartsRentals", x => new { x.ShoppingCartsId, x.RentalsId });
                    table.ForeignKey(
                        name: "FK_ShoppingCartsRentals_Rentals_RentalsId",
                        column: x => x.RentalsId,
                        principalTable: "Rentals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShoppingCartsRentals_ShoppingCarts_ShoppingCartsId",
                        column: x => x.ShoppingCartsId,
                        principalTable: "ShoppingCarts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartsRentals_RentalsId",
                table: "ShoppingCartsRentals",
                column: "RentalsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShoppingCartsRentals");

            migrationBuilder.AddColumn<string>(
                name: "RentalsId",
                table: "ShoppingCarts",
                type: "nvarchar(70)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_RentalsId",
                table: "ShoppingCarts",
                column: "RentalsId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCarts_Rentals_RentalsId",
                table: "ShoppingCarts",
                column: "RentalsId",
                principalTable: "Rentals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
