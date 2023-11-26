using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurante.Migrations
{
    public partial class comecardenovo2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Restaurants_AddresId",
                table: "Restaurants");

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_AddresId",
                table: "Restaurants",
                column: "AddresId");
        }
    }
}
