using CoinPurse.Data.Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace CoinPurse.Data.Postgres.Tests;

public class PostgresDatabaseTests : PostgresDatabaseContainerTest
{
    [Fact]
    public async Task Can_Store_Accounts()
    {
        await using var context = GetDbContext();
        int id;

        {
            var account = new Account { Name = "Test Account", Amount = 4800m };
            context.Accounts.Add(account);
            await context.SaveChangesAsync();
            id = account.AccountId;
        }

        {
            var account = await context.Accounts.FindAsync(id);
            account.Should().NotBeNull();
            account!.Name.Should().Be("Test Account");
            account.Amount.Should().Be(4800m);
        }
    }

    [Fact]
    public async Task Can_Store_Budgets()
    {
        await using var context = GetDbContext();
        int id;

        {
            var budget = new Budget { Name = "Test Budget"};
            context.Budgets.Add(budget);
            await context.SaveChangesAsync();
            id = budget.BudgetId;
        }

        {
            var account = await context.Budgets.FindAsync(id);
            account.Should().NotBeNull();
            account!.Name.Should().Be("Test Budget");
        }
    }

    [Fact]
    public async Task Can_Store_Budget_With_Accounts_And_Expenses()
    {
        await using var context = GetDbContext();
        int budgetId;

        {
            var budget = new Budget { Name = "Test Budget" };
            var account = new Account { Name = "Test Account", Amount = 4800m };
            var expense = new Expense { Name = "Test Expense", Amount = 200m };

            budget.AddAccount(account);
            budget.AddExpense(expense);

            context.Budgets.Add(budget);

            await context.SaveChangesAsync();

            budgetId = budget.BudgetId;
        }

        {
            var budget = await context.Budgets
                .Include(a => a.Expenses)
                .Include(a => a.Accounts)
                .SingleAsync(n => n.BudgetId == budgetId);

            budget.Should().NotBeNull();
            budget.Name.Should().Be("Test Budget");

            budget.Accounts.Should().HaveCount(1);
            budget.Accounts.First().Name.Should().Be("Test Account");
            budget.Accounts.First().Amount.Should().Be(4800m);

            budget.Expenses.Should().HaveCount(1);
            budget.Expenses.First().Name.Should().Be("Test Expense");
            budget.Expenses.First().Amount.Should().Be(200m);
        }
    }

    [Fact]
    public async Task Can_Store_Expense_With_Category()
    {
        await using var context = GetDbContext();
        int expenseId;

        {
            var budget = new Budget { Name = "Test Budget" };
            var category = new ExpenseCategory { Name = "Test Category" };
            var expense = new Expense { Name = "Test Expense", Amount = 200m, Category = category, Budget = budget };

            context.Expenses.Add(expense);
            await context.SaveChangesAsync();

            expenseId = expense.ExpenseId;
        }

        {
            var expense = await context.Expenses
                .Include(e => e.Category)
                .SingleAsync(e => e.ExpenseId == expenseId);

            expense.Should().NotBeNull();
            expense!.Name.Should().Be("Test Expense");
            expense.Amount.Should().Be(200m);

            expense.Category.Should().NotBeNull();
            expense.Category!.Name.Should().Be("Test Category");
        }
    }
}