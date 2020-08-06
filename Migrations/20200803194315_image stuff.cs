using Microsoft.EntityFrameworkCore.Migrations;

namespace Nomad_MVC.Migrations
{
    public partial class imagestuff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Cars",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Cars",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Cars");
        }
    }
}
