using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantDL.Migrations
{
    /// <inheritdoc />
    public partial class @new : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RestaurantEFResaurantID",
                table: "Tafel",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Verwijderd",
                table: "Tafel",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Verwijderd",
                table: "Restaurant",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Verwijderd",
                table: "Reservatie",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Verwijderd",
                table: "Gebruiker",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Tafel_RestaurantEFResaurantID",
                table: "Tafel",
                column: "RestaurantEFResaurantID");

            migrationBuilder.AddForeignKey(
                name: "FK_Tafel_Restaurant_RestaurantEFResaurantID",
                table: "Tafel",
                column: "RestaurantEFResaurantID",
                principalTable: "Restaurant",
                principalColumn: "ResaurantID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tafel_Restaurant_RestaurantEFResaurantID",
                table: "Tafel");

            migrationBuilder.DropIndex(
                name: "IX_Tafel_RestaurantEFResaurantID",
                table: "Tafel");

            migrationBuilder.DropColumn(
                name: "RestaurantEFResaurantID",
                table: "Tafel");

            migrationBuilder.DropColumn(
                name: "Verwijderd",
                table: "Tafel");

            migrationBuilder.DropColumn(
                name: "Verwijderd",
                table: "Restaurant");

            migrationBuilder.DropColumn(
                name: "Verwijderd",
                table: "Reservatie");

            migrationBuilder.DropColumn(
                name: "Verwijderd",
                table: "Gebruiker");
        }
    }
}
