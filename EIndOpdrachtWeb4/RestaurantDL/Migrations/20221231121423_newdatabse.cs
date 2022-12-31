using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantDL.Migrations
{
    /// <inheritdoc />
    public partial class newdatabse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Beschikbaar",
                table: "Tafel");

            migrationBuilder.AddColumn<int>(
                name: "TafelNummer",
                table: "Tafel",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TafelNummer",
                table: "Tafel");

            migrationBuilder.AddColumn<bool>(
                name: "Beschikbaar",
                table: "Tafel",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
