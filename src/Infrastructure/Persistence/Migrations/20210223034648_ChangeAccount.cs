using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyBills.Infrastructure.Persistence.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class ChangeAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Account",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Account",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Account",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Account");
        }
    }
}
