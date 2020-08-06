using Microsoft.EntityFrameworkCore.Migrations;

namespace Nomad_MVC.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_CarImage_CarImageId",
                table: "Cars");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_CarImage_CarImageId",
                table: "Cars",
                column: "CarImageId",
                principalTable: "CarImage",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_CarImage_CarImageId",
                table: "Cars");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_CarImage_CarImageId",
                table: "Cars",
                column: "CarImageId",
                principalTable: "CarImage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
