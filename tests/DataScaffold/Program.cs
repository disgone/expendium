// See https://aka.ms/new-console-template for more information

using DataScaffold;
using Microsoft.EntityFrameworkCore;

await using var context = new MssqlCoinDbContext();
//
// var budget = new Budget { Name = "Home Budget" };
// budget.Expenses.Add(new Expense
// {
//     Name = "Mortgage",
//     Amount = 1500m,
//     PaymentFrequency = PaymentFrequency.Monthly
// });
// budget.Expenses.Add(new Expense
// {
//     Name = "Maid",
//     Amount = 185m,
//     PaymentFrequency = PaymentFrequency.BiWeekly
// });
//
// budget.Accounts.Add(new Account { Name = "Tom Checking", Amount = 5600m });
//
// await context.Budgets.AddAsync(budget);
// await context.SaveChangesAsync();

var budget = await context.Budgets
    .Include(b => b.Accounts)
    .Include(b => b.Expenses)
    .FirstOrDefaultAsync(b => b.BudgetId == 1);

var totalContribution = budget.Accounts.Sum(n => n.Amount);
var totalExpenses = budget.Expenses.Sum(exp => exp.Amount);

Console.WriteLine($"Budget: {budget.Name}");
Console.WriteLine($"Total Contributions from Accounts: {totalContribution:C}");
Console.WriteLine($"Total Expenses: {totalExpenses:C}");

Console.WriteLine("Accounts and their contributions:");
foreach (var account in budget.Accounts)
{
    decimal share = account.Amount / totalContribution;
    Console.WriteLine($"{account.Name}: {account.Amount:C} ({share:P})");
}

Console.WriteLine("Expenses and each account's responsibility:");
foreach (var expense in budget.Expenses)
{
    Console.WriteLine($"Expense: {expense.Name}, Total Amount: {expense.Amount:C}");
    foreach (var account in budget.Accounts)
    {
        var share = account.Amount / totalContribution;
        var amountOwed = expense.Amount * share;
        Console.WriteLine($"{account.Name} owes {amountOwed:C2} for {expense.Name}");
    }
}