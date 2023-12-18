using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using CoinPurse.Data.Decorators;
using Microsoft.EntityFrameworkCore;

namespace CoinPurse.Data.Entities;

public class Expense : IIdentifiable<int>, IAudited
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ExpenseId { get; set; }

    [Required]
    [MaxLength(75)]
    public required string Name { get; set; }

    [Required]
    [Precision(18, 2)]
    public required decimal Amount { get; set; }

    [Required]
    [Column("PaymentFrequencyId")]
    public PaymentFrequency PaymentFrequency { get; set; } = PaymentFrequency.Monthly;

    [Required]
    public int BudgetId { get; set; }

    public Budget Budget { get; set; } = default!;

    public int? CategoryId { get; set; }

    public ExpenseCategory? Category { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset? ModifiedAt { get; set; }

    public int GetId() => ExpenseId;

    public void SetId(int id) => ExpenseId = id;
}