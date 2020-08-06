using Microsoft.EntityFrameworkCore.Migrations;

namespace Nomad_MVC.Migrations
{
    public partial class tst : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CarImageId",
                table: "Cars",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CarImage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
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
                principalColumn: "Id");
        }
    }
}
