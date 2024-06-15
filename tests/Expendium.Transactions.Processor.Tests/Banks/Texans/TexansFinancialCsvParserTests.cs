using System.Text;
using Expendium.Transactions.Processing.Banks.Texans;

namespace Expendium.Transactions.Processor.Tests.Banks.Texans;

public class TexansFinancialCsvParserTests
{
    private readonly TexansFinancialCsvParser _parser;

    public TexansFinancialCsvParserTests()
    {
        _parser = new TexansFinancialCsvParser();
    }

    [Fact]
    [Trait("Category", "Integration")]
    public void Parse_HasRecords_ReturnsTransactions()
    {
        using var stream =
            TestFileHelper.GetFileStream("Banks/Texans/Transactions.csv");

        var result = _parser.Parse(stream).ToList();

        result.Should().HaveCount(5);
    }

    [Fact]
    public void Parse_WhenStreamHasValidRow_ReturnsOneTransaction()
    {
        using var stream = new MemoryStream();
        using var writer = new StreamWriter(stream);
        writer.WriteLine(
            "\"Transaction ID\",\"Posting Date\",\"Effective Date\",\"Transaction Type\",\"Amount\",\"Check Number\",\"Reference Number\",\"Description\",\"Transaction Category\",\"Type\",\"Balance\",\"Memo\",\"Extended Description\"");
        writer.WriteLine(
            "\"20240401 210000 484,000 178,132\",\"4/1/2024\",\"4/2/2024\",\"Credit\",\"4840.00000\",\"\",\"122498712\",\"External ACH Trace 221\",\"Other Income\",\"ACH\",\"10292.69000\",\"\",\"External ACH Trace 221: Payment Processing\"");
        writer.Flush();
        stream.Position = 0;

        var result = _parser.Parse(stream).ToList();

        result.Should().ContainSingle();

        var transaction = result[0] as TexansFinancialTransaction;
        transaction!.TransactionSignature.Should().Be("20240401 210000 484,000 178,132");
        transaction.PostingDate.Should().Be(DateTimeOffset.Parse("4/1/2024"));
        transaction.TransactionDate.Should().Be(DateTimeOffset.Parse("4/2/2024"));
        transaction.TransactionType.Should().Be("Credit");
        transaction.Amount.Should().Be(4840.00000m);
        transaction.ReferenceNumber.Should().Be("122498712");
        transaction.Description.Should().Be("External ACH Trace 221");
        transaction.Category.Should().Be("Other Income");
        transaction.Type.Should().Be("ACH");
        transaction.Balance.Should().Be(10292.69000m);
        transaction.ExtendedDescription.Should()
            .Be("External ACH Trace 221: Payment Processing");
    }

    [Fact]
    public void Parse_EmptyCsv_ReturnsEmptyList()
    {
        var emptyCsv = "";
        using var stream = new MemoryStream(Encoding.UTF8.GetBytes(emptyCsv));

        var result = _parser.Parse(stream).ToList();

        result.Should().BeEmpty();
    }
}