using Microsoft.EntityFrameworkCore.Migrations;

namespace CinemaShop.Web.Data.Migrations
{
    public partial class updateone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "date",
                table: "Products",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "date",
                table: "Products");
        }
    }
}
