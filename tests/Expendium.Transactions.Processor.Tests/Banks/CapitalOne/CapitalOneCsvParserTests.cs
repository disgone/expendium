using Expendium.Transactions.Processing.Banks.CapitalOne;

namespace Expendium.Transactions.Processor.Tests.Banks.CapitalOne;

public class CapitalOneCsvParserTests
{
    [Fact]
    public void Parse_WhenStreamIsEmpty_ReturnsEmptyCollection()
    {
        var parser = new CapitalOneCsvParser();

        var result = parser.Parse(Stream.Null);

        result.Should().BeEmpty();
    }

    [Fact]
    public void Parse_WhenStreamHasDebitRow_ReturnsOneTransactionWithNegativeAmount()
    {
        var parser = new CapitalOneCsvParser();
        using var stream = new MemoryStream();
        using var writer = new StreamWriter(stream);
        writer.WriteLine("Transaction Date,Posted Date,Card No.,Description,Category,Debit,Credit");
        writer.WriteLine("2021-01-01,2021-01-02,1234,Test,Test Category,1.87,");
        writer.Flush();
        stream.Position = 0;

        var result = parser.Parse(stream).ToList();

        result.Should().ContainSingle();

        var transaction =
            result[0].Should().BeAssignableTo<CapitalOneTransaction>().Which;
        transaction.TransactionDate.Should().Be(new DateTimeOffset(2021, 1, 1, 0, 0, 0, TimeSpan.Zero));
        transaction.PostedDate.Should().Be(new DateTimeOffset(2021, 1, 2, 0, 0, 0, TimeSpan.Zero));
        transaction.CardNumber.Should().Be("1234");
        transaction.Description.Should().Be("Test");
        transaction.Category.Should().Be("Test Category");
        transaction.Amount.Should().Be(-1.87m);
    }

    [Fact]
    public void Parse_WhenStreamHasCreditRow_ReturnsOneTransaction()
    {
        var parser = new CapitalOneCsvParser();
        using var stream = new MemoryStream();
        using var writer = new StreamWriter(stream);
        writer.WriteLine("Transaction Date,Posted Date,Card No.,Description,Category,Debit,Credit");
        writer.WriteLine("2021-01-01,2021-01-02,1234,Test,Test Cat,,887.21");
        writer.Flush();
        stream.Position = 0;

        var result = parser.Parse(stream).ToList();

        var transaction =
            result[0].Should().BeAssignableTo<CapitalOneTransaction>().Which;
        transaction.TransactionDate.Should().Be(new DateTimeOffset(2021, 1, 1, 0, 0, 0, TimeSpan.Zero));
        transaction.PostedDate.Should().Be(new DateTimeOffset(2021, 1, 2, 0, 0, 0, TimeSpan.Zero));
        transaction.CardNumber.Should().Be("1234");
        transaction.Description.Should().Be("Test");
        transaction.Category.Should().Be("Test Cat");
        transaction.Amount.Should().Be(887.21m);
    }

    [Fact]
    [Trait("Category", "Integration")]
    public void Parse_WhenStreamContainsCsvData_ReturnsTransactions()
    {
        var parser = new CapitalOneCsvParser();
        using var stream = TestFileHelper.GetFileStream("Banks/CapitalOne/Transactions.csv");

        var result = parser.Parse(stream).ToList();

        result.Should().HaveCount(12);

        var credits = result.Where(n => n.Amount > 0).ToList();
        credits.Should().HaveCount(2);
        credits.Sum(n => n.Amount).Should().Be(305.15m);

        var debits = result.Where(n => n.Amount < 0).ToList();
        debits.Should().HaveCount(10);
        debits.Sum(n => n.Amount).Should().Be(-676.30m);
    }
}