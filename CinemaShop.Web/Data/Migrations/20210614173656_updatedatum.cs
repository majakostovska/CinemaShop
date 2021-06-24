using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CinemaShop.Web.Data.Migrations
{
    public partial class updatedatum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "date",
                table: "Products");

            migrationBuilder.AddColumn<DateTime>(
                name: "data",
                table: "Products",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "data",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "date",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
