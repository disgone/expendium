using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoinPurse.Data.Entities;

public class ExpenseCategory
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ExpenseCategoryId { get; set; }

    [Required]
    [MaxLength(75)]
    public required string Name { get; set; }

    public ICollection<Expense> Expenses { get; set; } = Array.Empty<Expense>();
}