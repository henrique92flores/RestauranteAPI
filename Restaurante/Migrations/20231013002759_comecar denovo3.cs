using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurante.Migrations
{
    public partial class comecardenovo3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Restaurants_AddresId",
                table: "Restaurants");

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_AddresId",
                table: "Restaurants",
                column: "AddresId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Restaurants_AddresId",
                table: "Restaurants");

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_AddresId",
                table: "Restaurants",
                column: "AddresId",
                unique: true);
        }
    }
}
