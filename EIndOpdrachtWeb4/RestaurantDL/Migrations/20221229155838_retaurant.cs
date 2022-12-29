using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantDL.Migrations
{
    /// <inheritdoc />
    public partial class retaurant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservatie_Restaurant_RestaurantInfoResaurantID",
                table: "Reservatie");

            migrationBuilder.DropForeignKey(
                name: "FK_Tafel_Restaurant_RestaurantEFResaurantID",
                table: "Tafel");

            migrationBuilder.RenameColumn(
                name: "RestaurantEFResaurantID",
                table: "Tafel",
                newName: "RestaurantEFRestaurantID");

            migrationBuilder.RenameIndex(
                name: "IX_Tafel_RestaurantEFResaurantID",
                table: "Tafel",
                newName: "IX_Tafel_RestaurantEFRestaurantID");

            migrationBuilder.RenameColumn(
                name: "ResaurantID",
                table: "Restaurant",
                newName: "RestaurantID");

            migrationBuilder.RenameColumn(
                name: "RestaurantInfoResaurantID",
                table: "Reservatie",
                newName: "RestaurantInfoRestaurantID");

            migrationBuilder.RenameIndex(
                name: "IX_Reservatie_RestaurantInfoResaurantID",
                table: "Reservatie",
                newName: "IX_Reservatie_RestaurantInfoRestaurantID");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservatie_Restaurant_RestaurantInfoRestaurantID",
                table: "Reservatie",
                column: "RestaurantInfoRestaurantID",
                principalTable: "Restaurant",
                principalColumn: "RestaurantID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tafel_Restaurant_RestaurantEFRestaurantID",
                table: "Tafel",
                column: "RestaurantEFRestaurantID",
                principalTable: "Restaurant",
                principalColumn: "RestaurantID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservatie_Restaurant_RestaurantInfoRestaurantID",
                table: "Reservatie");

            migrationBuilder.DropForeignKey(
                name: "FK_Tafel_Restaurant_RestaurantEFRestaurantID",
                table: "Tafel");

            migrationBuilder.RenameColumn(
                name: "RestaurantEFRestaurantID",
                table: "Tafel",
                newName: "RestaurantEFResaurantID");

            migrationBuilder.RenameIndex(
                name: "IX_Tafel_RestaurantEFRestaurantID",
                table: "Tafel",
                newName: "IX_Tafel_RestaurantEFResaurantID");

            migrationBuilder.RenameColumn(
                name: "RestaurantID",
                table: "Restaurant",
                newName: "ResaurantID");

            migrationBuilder.RenameColumn(
                name: "RestaurantInfoRestaurantID",
                table: "Reservatie",
                newName: "RestaurantInfoResaurantID");

            migrationBuilder.RenameIndex(
                name: "IX_Reservatie_RestaurantInfoRestaurantID",
                table: "Reservatie",
                newName: "IX_Reservatie_RestaurantInfoResaurantID");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservatie_Restaurant_RestaurantInfoResaurantID",
                table: "Reservatie",
                column: "RestaurantInfoResaurantID",
                principalTable: "Restaurant",
                principalColumn: "ResaurantID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tafel_Restaurant_RestaurantEFResaurantID",
                table: "Tafel",
                column: "RestaurantEFResaurantID",
                principalTable: "Restaurant",
                principalColumn: "ResaurantID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
