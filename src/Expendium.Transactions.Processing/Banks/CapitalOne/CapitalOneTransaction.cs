using System.Security.Cryptography;
using System.Text;
using Expendium.Transactions.Abstractions;

namespace Expendium.Transactions.Processing.Banks.CapitalOne;

public record CapitalOneTransaction : ITransaction
{
    private const string CapitalOneBankName = "CapitalOne";

    private string? _transactionSignature;
    private string? _sourceAccount;

    public string SourceAccount => _sourceAccount ??= $"{BankName}:{CardNumber}";

    public string BankName => CapitalOneBankName;

    public string TransactionSignature => _transactionSignature ??= GenerateHash();

    public DateTimeOffset TransactionDate { get; init; }

    public decimal Amount { get; init; }

    public string Description { get; init; } = string.Empty;

    public string? Category { get; init; }

    public ICollection<string> Tags { get; init; } = Array.Empty<string>();

    public DateTimeOffset PostedDate { get; init; }

    public string CardNumber { get; init; } = string.Empty;

    private string GenerateHash()
    {
        StringBuilder transactionSb = new StringBuilder();

        // Glob our identifying ata together separated by :
        transactionSb.AppendJoin(":",
            SourceAccount,
            TransactionDate.ToString("O"),
            PostedDate.ToString("O"),
            Amount,
            Description);

        // Compute the hash
        byte[] data = SHA256.HashData(Encoding.UTF8.GetBytes(transactionSb.ToString()));

        string hash = BitConverter.ToString(data).Replace("-", string.Empty).ToLowerInvariant();

        return hash;
    }
}