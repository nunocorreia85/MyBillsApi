using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyBills.Infrastructure.Persistence.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class UpdateAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Account",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Account",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Account",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "Account",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "Account");
        }
    }
}
