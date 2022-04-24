using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieStoreRentalService.Data.Migrations
{
    public partial class AddedMovieDirector : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MovieDirectorId",
                table: "Rentals",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "MovieDirectors",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieDirectors", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_MovieDirectorId",
                table: "Rentals",
                column: "MovieDirectorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_MovieDirectors_MovieDirectorId",
                table: "Rentals",
                column: "MovieDirectorId",
                principalTable: "MovieDirectors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_MovieDirectors_MovieDirectorId",
                table: "Rentals");

            migrationBuilder.DropTable(
                name: "MovieDirectors");

            migrationBuilder.DropIndex(
                name: "IX_Rentals_MovieDirectorId",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "MovieDirectorId",
                table: "Rentals");
        }
    }
}
