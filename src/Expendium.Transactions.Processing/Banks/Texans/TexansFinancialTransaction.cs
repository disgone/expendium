using Expendium.Transactions.Abstractions;

namespace Expendium.Transactions.Processing.Banks.Texans;

public record TexansFinancialTransaction : ITransaction
{
    private const string TexansBankName = "Texans";

    public string SourceAccount { get; init; }
    public string BankName => TexansBankName;
    public string TransactionSignature { get; init; }
    public DateTimeOffset TransactionDate { get; init; }
    public decimal Amount { get; init; }
    public string Description { get; init; }
    public string? Category { get; init; }
    public ICollection<string> Tags { get; init; } = Array.Empty<string>();
    public string Type { get; init; }
    public DateTimeOffset PostingDate { get; init; }
    public string? CheckNumber { get; init; }
    public string ReferenceNumber { get; init; }
    public string TransactionType { get; init; }
    public decimal Balance { get; init; }
    public string? Memo { get; init; }
    public string ExtendedDescription { get; init; }
}