using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyBills.Infrastructure.Persistence.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Account",
                table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    LastModified = table.Column<DateTime>(nullable: true),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    OwnerName = table.Column<string>(maxLength: 200, nullable: false),
                    Balance = table.Column<decimal>(nullable: false),
                    BankAccountNumber = table.Column<string>(nullable: true),
                    Closed = table.Column<bool>(nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_Account", x => x.Id); });

            migrationBuilder.CreateTable(
                "TransactionCategory",
                table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    LastModified = table.Column<DateTime>(nullable: true),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Description = table.Column<string>(nullable: true),
                    RecurringPeriod = table.Column<int>(nullable: true),
                    AccountId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionCategory", x => x.Id);
                    table.ForeignKey(
                        "FK_TransactionCategory_Account_AccountId",
                        x => x.AccountId,
                        "Account",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "BankTransaction",
                table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    LastModified = table.Column<DateTime>(nullable: true),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    Memo = table.Column<string>(maxLength: 200, nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    CategoryId = table.Column<long>(nullable: false),
                    AccountId = table.Column<long>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    TransactionCategoryId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankTransaction", x => x.Id);
                    table.ForeignKey(
                        "FK_BankTransaction_Account_AccountId",
                        x => x.AccountId,
                        "Account",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_BankTransaction_TransactionCategory_TransactionCategoryId",
                        x => x.TransactionCategoryId,
                        "TransactionCategory",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                "IX_BankTransaction_AccountId",
                "BankTransaction",
                "AccountId");

            migrationBuilder.CreateIndex(
                "IX_BankTransaction_TransactionCategoryId",
                "BankTransaction",
                "TransactionCategoryId");

            migrationBuilder.CreateIndex(
                "IX_TransactionCategory_AccountId",
                "TransactionCategory",
                "AccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "BankTransaction");

            migrationBuilder.DropTable(
                "TransactionCategory");

            migrationBuilder.DropTable(
                "Account");
        }
    }
}