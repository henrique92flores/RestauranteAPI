using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurante.Migrations
{
    public partial class comecardenovo4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Foods_RestaurantId",
                table: "Foods",
                column: "RestaurantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Foods_Restaurants_RestaurantId",
                table: "Foods",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Foods_Restaurants_RestaurantId",
                table: "Foods");

            migrationBuilder.DropIndex(
                name: "IX_Foods_RestaurantId",
                table: "Foods");
        }
    }
}
