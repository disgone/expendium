using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Expendium.Data.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class AddCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_ExpenseCategory_CategoryExpenseCategoryId",
                table: "Expenses");

            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_ExpenseCategory_CategoryId",
                table: "Expenses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExpenseCategory",
                table: "ExpenseCategory");

            migrationBuilder.RenameTable(
                name: "ExpenseCategory",
                newName: "Categories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "ExpenseCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Categories_CategoryExpenseCategoryId",
                table: "Expenses",
                column: "CategoryExpenseCategoryId",
                principalTable: "Categories",
                principalColumn: "ExpenseCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Categories_CategoryId",
                table: "Expenses",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "ExpenseCategoryId",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Categories_CategoryExpenseCategoryId",
                table: "Expenses");

            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Categories_CategoryId",
                table: "Expenses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "ExpenseCategory");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExpenseCategory",
                table: "ExpenseCategory",
                column: "ExpenseCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_ExpenseCategory_CategoryExpenseCategoryId",
                table: "Expenses",
                column: "CategoryExpenseCategoryId",
                principalTable: "ExpenseCategory",
                principalColumn: "ExpenseCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_ExpenseCategory_CategoryId",
                table: "Expenses",
                column: "CategoryId",
                principalTable: "ExpenseCategory",
                principalColumn: "ExpenseCategoryId",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
