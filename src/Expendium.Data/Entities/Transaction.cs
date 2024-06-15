using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Expendium.Data.Decorators;
using Expendium.Transactions.Abstractions;

namespace Expendium.Data.Entities;

public class Transaction : ITransaction, IIdentifiable<long>, IAudited
{
    private readonly List<string> _tags = [];

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long TransactionId { get; set; }

    [MaxLength(400)]
    public required string SourceAccount { get; set; }

    [Required]
    [MaxLength(400)]
    public required string BankName { get; set; }

    [Required]
    [MaxLength(250)]
    public required string TransactionSignature { get; set; }

    public required DateTimeOffset TransactionDate { get; set; }

    [Required]
    public required decimal Amount { get; set; }

    [MaxLength(400)]
    public string? Description { get; set; }

    [MaxLength(200)]
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