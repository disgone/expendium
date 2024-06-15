using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Expendium.Data.Decorators;

namespace Expendium.Data.Entities;

public class Budget : IIdentifiable<int>, IAudited
{
    private readonly List<Account> _accounts = new();
    private readonly List<Expense> _expenses = new();

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int BudgetId { get; set; }

    [Required]
    [MaxLength(100)]
    public required string Name { get; set; }

    public IReadOnlyCollection<Account> Accounts => _accounts.ToList();

    public IReadOnlyCollection<Expense> Expenses => _expenses.ToList();

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset? ModifiedAt { get; set; }

    public int GetId() => BudgetId;

    public void SetId(int id) => BudgetId = id;

    public void AddAccount(Account account)
    {
        ArgumentNullException.ThrowIfNull(account);
        if (!_accounts.Any(a => a.AccountId == account.AccountId))
        {
            _accounts.Add(account);
        }
    }

    public void RemoveAccount(Account account)
    {
        ArgumentNullException.ThrowIfNull(account);
        if (_accounts.Any(a => a.AccountId == account.AccountId))
        {
            _accounts.Remove(account);
        }
    }

    public void AddExpense(Expense expense)
    {
        ArgumentNullException.ThrowIfNull(expense);
        if (!_expenses.Any(a => a.ExpenseId == expense.ExpenseId))
        {
            _expenses.Add(expense);
        }
    }

    public void RemoveExpense(Expense expense)
    {
        ArgumentNullException.ThrowIfNull(expense);
        if (!_expenses.Any(a => a.ExpenseId == expense.ExpenseId))
        {
            _expenses.Add(expense);
        }
    }
}