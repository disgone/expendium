using System.Globalization;
using Expendium.Transactions.Abstractions;
using nietras.SeparatedValues;

namespace Expendium.Transactions.Processing.Banks.Texans;

public class TexansFinancialCsvParser : ITransactionParser
{
    public string Format => ParserFormats.Csv.Name;
    public IEnumerable<ITransaction> Parse(Stream stream)
    {
        using var reader = Sep.New(',')
            .Reader(o => o with
            {
                HasHeader = true,
                DisableColCountCheck = true
            })
            .From(stream);

        var headers = reader.Header.ColNames.Select(h => h.Trim('"')).ToList();

        foreach (var row in reader)
        {
            if (row.Span.Length == 0) continue;

            var transaction = new TexansFinancialTransaction
            {
                TransactionSignature = ReadColumnValue(row, headers.IndexOf("Transaction ID")).ToString(),
                TransactionDate =
                    DateTimeOffset.ParseExact(
                        ReadColumnValue(row, headers.IndexOf("Effective Date")).ToString(), "M/d/yyyy",
                        CultureInfo.InvariantCulture),
                PostingDate =
                    DateTimeOffset.ParseExact(
                        ReadColumnValue(row, headers.IndexOf("Posting Date")).ToString(), "M/d/yyyy",
                        CultureInfo.InvariantCulture),
                TransactionType = ReadColumnValue(row, headers.IndexOf("Transaction Type")).ToString(),
                Amount =
                    decimal.Parse(
                        ReadColumnValue(row, headers.IndexOf("Amount")).ToString(),
                        CultureInfo.InvariantCulture),
                CheckNumber =
                    ReadColumnValue(row, headers.IndexOf("Check Number")).ToString(),
                ReferenceNumber =
                    ReadColumnValue(row, headers.IndexOf("Reference Number")).ToString(),
                Description =
                    ReadColumnValue(row, headers.IndexOf("Description")).ToString(),
                Category = ReadColumnValue(row, headers.IndexOf("Transaction Category")).ToString(),
                Type = ReadColumnValue(row, headers.IndexOf("Type")).ToString(),
                Balance =
                    decimal.Parse(
                        ReadColumnValue(row, headers.IndexOf("Balance")).ToString(),
                        CultureInfo.InvariantCulture),
                Memo = ReadColumnValue(row, headers.IndexOf("Memo")).ToString(),
                ExtendedDescription = ReadColumnValue(row, headers.IndexOf("Extended Description")).ToString()
            };

            yield return transaction;
        }
    }

    private static ReadOnlySpan<char> ReadColumnValue(SepReader.Row row, int columnIndex)
    {
        var span = row[columnIndex].Span;
        return span.Length >= 2 && span[0] == '"' && span[^1] == '"'
            ? span.Slice(1, span.Length - 2)
            : span;
    }
}