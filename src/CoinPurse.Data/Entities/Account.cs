using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using CoinPurse.Data.Decorators;
using Microsoft.EntityFrameworkCore;

namespace CoinPurse.Data.Entities;

public class Account : IIdentifiable<int>, IAudited
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int AccountId { get; set; }

    [Required]
    [MaxLength(120)]
    public required string Name { get; set; }

    [Required]
    [Precision(18,2)]
    public required decimal Amount { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset? ModifiedAt { get; set; }

    public ICollection<Budget> Budgets { get; set; } = new List<Budget>();

    public int GetId() => AccountId;

    public void SetId(int id) => AccountId = id;
}