using Expendium.Transactions.Abstractions;

namespace Expendium.Transactions.Processing;

public interface ITransactionParser
{
    string Format { get; }
    IEnumerable<ITransaction> Parse(Stream stream);
}

public sealed class ParserFormats
{
    public static readonly ParserFormats Csv = new("CSV", ".csv");

    private ParserFormats(string name, string extension)
    {
        Name = name;
        Extension = extension;
    }

    public string Extension { get; }

    public string Name { get; }
}