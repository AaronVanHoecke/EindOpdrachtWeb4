using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantDL.Migrations
{
    /// <inheritdoc />
    public partial class fixTafelTabel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tafel_Restaurant_RestaurantEFRestaurantID",
                table: "Tafel");

            migrationBuilder.DropIndex(
                name: "IX_Tafel_RestaurantEFRestaurantID",
                table: "Tafel");

            migrationBuilder.DropColumn(
                name: "RestaurantEFRestaurantID",
                table: "Tafel");

            migrationBuilder.CreateIndex(
                name: "IX_Tafel_RestaurantID",
                table: "Tafel",
                column: "RestaurantID");

            migrationBuilder.AddForeignKey(
                name: "FK_Tafel_Restaurant_RestaurantID",
                table: "Tafel",
                column: "RestaurantID",
                principalTable: "Restaurant",
                principalColumn: "RestaurantID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tafel_Restaurant_RestaurantID",
                table: "Tafel");

            migrationBuilder.DropIndex(
                name: "IX_Tafel_RestaurantID",
                table: "Tafel");

            migrationBuilder.AddColumn<int>(
                name: "RestaurantEFRestaurantID",
                table: "Tafel",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tafel_RestaurantEFRestaurantID",
                table: "Tafel",
                column: "RestaurantEFRestaurantID");

            migrationBuilder.AddForeignKey(
                name: "FK_Tafel_Restaurant_RestaurantEFRestaurantID",
                table: "Tafel",
                column: "RestaurantEFRestaurantID",
                principalTable: "Restaurant",
                principalColumn: "RestaurantID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
