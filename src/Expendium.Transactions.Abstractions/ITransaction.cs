namespace Expendium.Transactions.Abstractions;

/// <summary>
/// Represents a transaction.
/// </summary>
public interface ITransaction
{
    /// <summary>
    /// Gets the signature for the transaction, which can be used to identify unique or
    /// like transactions.
    /// </summary>
    string TransactionSignature { get; }

    /// <summary>
    /// Gets the date and time of the transaction.
    /// </summary>
    DateTimeOffset TransactionDate { get; }

    /// <summary>
    /// Gets the amount of the transaction.
    /// </summary>
    decimal Amount { get; }

    /// <summary>
    /// Gets the description of the transaction.
    /// </summary>
    string Description { get; }

    /// <summary>
    /// Gets the category of the transaction.
    /// </summary>
    string? Category { get; }

    /// <summary>
    /// Gets the list of tags associated with the transaction.
    /// </summary>
    ICollection<string> Tags { get; }
}