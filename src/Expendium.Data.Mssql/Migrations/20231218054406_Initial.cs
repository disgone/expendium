using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CoinPurse.Data.Mssql.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AccountId);
                });

            migrationBuilder.CreateTable(
                name: "Budgets",
                columns: table => new
                {
                    BudgetId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Budgets", x => x.BudgetId);
                });

            migrationBuilder.CreateTable(
                name: "ExpenseCategory",
                columns: table => new
                {
                    ExpenseCategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseCategory", x => x.ExpenseCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "PaymentFrequencyTypes",
                columns: table => new
                {
                    PaymentFrequencyId = table.Column<short>(type: "smallint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentFrequencyTypes", x => x.PaymentFrequencyId);
                });

            migrationBuilder.CreateTable(
                name: "AccountBudget",
                columns: table => new
                {
                    AccountsAccountId = table.Column<int>(type: "int", nullable: false),
                    BudgetsBudgetId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountBudget", x => new { x.AccountsAccountId, x.BudgetsBudgetId });
                    table.ForeignKey(
                        name: "FK_AccountBudget_Accounts_AccountsAccountId",
                        column: x => x.AccountsAccountId,
                        principalTable: "Accounts",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountBudget_Budgets_BudgetsBudgetId",
                        column: x => x.BudgetsBudgetId,
                        principalTable: "Budgets",
                        principalColumn: "BudgetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    ExpenseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PaymentFrequencyId = table.Column<short>(type: "smallint", nullable: false, defaultValue: (short)4),
                    BudgetId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    CategoryExpenseCategoryId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.ExpenseId);
                    table.ForeignKey(
                        name: "FK_Expenses_Budgets_BudgetId",
                        column: x => x.BudgetId,
                        principalTable: "Budgets",
                        principalColumn: "BudgetId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Expenses_ExpenseCategory_CategoryExpenseCategoryId",
                        column: x => x.CategoryExpenseCategoryId,
                        principalTable: "ExpenseCategory",
                        principalColumn: "ExpenseCategoryId");
                    table.ForeignKey(
                        name: "FK_Expenses_ExpenseCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ExpenseCategory",
                        principalColumn: "ExpenseCategoryId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Expenses_PaymentFrequencyTypes_PaymentFrequencyId",
                        column: x => x.PaymentFrequencyId,
                        principalTable: "PaymentFrequencyTypes",
                        principalColumn: "PaymentFrequencyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ExpenseCategory",
                columns: new[] { "ExpenseCategoryId", "Name" },
                values: new object[,]
                {
                    { 1, "Housing" },
                    { 2, "Utilities" },
                    { 3, "Food" },
                    { 4, "Savings & Investment" },
                    { 5, "Entertainment" },
                    { 6, "Miscellaneous" },
                    { 7, "Transportation" },
                    { 8, "Personal Services" }
                });

            migrationBuilder.InsertData(
                table: "PaymentFrequencyTypes",
                columns: new[] { "PaymentFrequencyId", "Name" },
                values: new object[,]
                {
                    { (short)1, "Daily" },
                    { (short)2, "Weekly" },
                    { (short)3, "BiWeekly" },
                    { (short)4, "Monthly" },
                    { (short)5, "Quarterly" },
                    { (short)6, "SemiAnnual" },
                    { (short)7, "Annually" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountBudget_BudgetsBudgetId",
                table: "AccountBudget",
                column: "BudgetsBudgetId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_BudgetId",
                table: "Expenses",
                column: "BudgetId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_CategoryExpenseCategoryId",
                table: "Expenses",
                column: "CategoryExpenseCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_CategoryId",
                table: "Expenses",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_PaymentFrequencyId",
                table: "Expenses",
                column: "PaymentFrequencyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountBudget");

            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Budgets");

            migrationBuilder.DropTable(
                name: "ExpenseCategory");

            migrationBuilder.DropTable(
                name: "PaymentFrequencyTypes");
        }
    }
}
