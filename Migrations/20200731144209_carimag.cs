using Microsoft.EntityFrameworkCore.Migrations;

namespace Nomad_MVC.Migrations
{
    public partial class carimag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Cars");

            migrationBuilder.AddColumn<int>(
                name: "CarImageId",
                table: "Cars",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CarImage",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<byte[]>(nullable: true),
                    FileName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarImage", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_CarImageId",
                table: "Cars",
                column: "CarImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_CarImage_CarImageId",
                table: "Cars",
                column: "CarImageId",
                principalTable: "CarImage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_CarImage_CarImageId",
                table: "Cars");

            migrationBuilder.DropTable(
                name: "CarImage");

            migrationBuilder.DropIndex(
                name: "IX_Cars_CarImageId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "CarImageId",
                table: "Cars");

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Cars",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
