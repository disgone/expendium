using System.Globalization;
using Expendium.Transactions.Abstractions;
using nietras.SeparatedValues;

namespace Expendium.Transactions.Processing.Banks.CapitalOne;

public class CapitalOneCsvParser : ITransactionParser
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

        foreach (var row in reader)
        {
            if (row.Span.Length == 0) continue;

            var transaction = new CapitalOneTransaction
            {
                TransactionDate =
                    DateTimeOffset.ParseExact(row["Transaction Date"].Span, "yyyy-MM-dd",
                        CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal),
                PostedDate =
                    DateTimeOffset.ParseExact(row["Posted Date"].Span, "yyyy-MM-dd",
                        CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal),
                CardNumber = row["Card No."].ToString(),
                Description = row["Description"].ToString(),
                Category = row["Category"].ToString(),
                Amount = TryParseAmount(row)
            };


            yield return transaction;
        }
    }

    private static decimal TryParseAmount(SepReader.Row value)
    {
        if (value["Debit"].Span.Length > 0)
        {
            return -decimal.Parse(value["Debit"].Span, CultureInfo.InvariantCulture);
        }

        if (value["Credit"].Span.Length > 0)
        {
            return decimal.Parse(value["Credit"].Span, CultureInfo.InvariantCulture);
        }

        return 0;
    }
}