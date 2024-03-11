using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Expendium.Data.Decorators;
using Expendium.Transactions.Abstractions;

namespace Expendium.Data.Entities;

public class Transaction : ITransaction, IIdentifiable<long>, IAudited
{
    private readonly List<string> _tags = new();

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long TransactionId { get; set; }

    public required string SourceAccount { get; set; }

    [Required]
    [MaxLength(400)]
    public required string BankName { get; set; }

    [Required]
    public required string TransactionSignature { get; set; }

    public required DateTimeOffset TransactionDate { get; set; }

    [Required]
    public required decimal Amount { get; set; }

    public string Description { get; set; }

    public string? Category { get; set; }

    [Required]
    public TransactionStatus Status { get; set; }

    public ICollection<string> Tags => _tags;

    public long GetId() => TransactionId;

    public void SetId(long id) => TransactionId = id;

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset? ModifiedAt { get; set; }
}

public enum TransactionStatus
{
    Imported = 0,
    Cleared,
    Rejected
}