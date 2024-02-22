using Expendium.Transactions.Processing.Banks.CapitalOne;

namespace Expendium.Transactions.Processor.Tests.Banks.CapitalOne;

public class CapitalOneTransactionTests
{
    [Fact]
    public void TransactionSignature_SameValuesHaveSameSignature()
    {
        var alpha = new CapitalOneTransaction
        {
            TransactionDate = new DateTimeOffset(2021, 1, 1, 0, 0, 0, TimeSpan.Zero),
            PostedDate = new DateTimeOffset(2021, 1, 2, 0, 0, 0, TimeSpan.Zero),
            Amount = -1.87m,
            Description = "Test",
            CardNumber = "1234"
        };

        var beta = new CapitalOneTransaction
        {
            TransactionDate = new DateTimeOffset(2021, 1, 1, 0, 0, 0, TimeSpan.Zero),
            PostedDate = new DateTimeOffset(2021, 1, 2, 0, 0, 0, TimeSpan.Zero),
            Amount = -1.87m,
            Description = "Test",
            CardNumber = "1234"
        };

        alpha.TransactionSignature.Should().Be(beta.TransactionSignature);
    }

    [Fact]
    public void TransactionSignature_ChangingTransactionDateChangesSignature()
    {
        var original = CreateDefaultTransaction();
        var update = original with
        {
            TransactionDate = original.TransactionDate.AddDays(1)
        };

        original.TransactionSignature.Should()
            .NotBe(update.TransactionSignature);
    }

    [Fact]
    public void TransactionSignature_ChangingPostedDateChangesSignature()
    {
        var original = CreateDefaultTransaction();
        var update = original with
        {
            PostedDate = original.PostedDate.AddDays(1)
        };

        original.TransactionSignature.Should()
            .NotBe(update.TransactionSignature);
    }

    [Fact]
    public void TransactionSignature_ChangingAmountChangesSignature()
    {
        var original = CreateDefaultTransaction();
        var update = original with { Amount = original.Amount + 0.01m };

        original.TransactionSignature.Should()
            .NotBe(update.TransactionSignature);
    }

    [Fact]
    public void TransactionSignature_ChangingDescriptionChangesSignature()
    {
        var original = CreateDefaultTransaction();
        var update = original with { Description = "Changed" };

        original.TransactionSignature.Should()
            .NotBe(update.TransactionSignature);
    }

    [Fact]
    public void TransactionSignature_ChangingCardNumberChangesSignature()
    {
        var original = CreateDefaultTransaction();
        var update = original with { CardNumber = "1235" };

        original.TransactionSignature.Should()
            .NotBe(update.TransactionSignature);
    }

    [Fact]
    public void TransactionSignature_ChangingCategoryDoesNotChangeSignature()
    {
        var original = CreateDefaultTransaction();
        var update = original with { Category = "Changed" };

        original.TransactionSignature.Should()
            .Be(update.TransactionSignature);
    }

    [Fact]
    public void TransactionSignature_ChangingTagsDoesNotChangeSignature()
    {
        var original = CreateDefaultTransaction();
        var update = original with { Tags = new List<string> { "A", "B" } };

        original.TransactionSignature.Should()
            .Be(update.TransactionSignature);
    }

    private CapitalOneTransaction CreateDefaultTransaction()
    {
        return new CapitalOneTransaction
        {
            TransactionDate = new DateTimeOffset(2021, 1, 1, 0, 0, 0, TimeSpan.Zero),
            PostedDate = new DateTimeOffset(2021, 1, 2, 0, 0, 0, TimeSpan.Zero),
            Amount = -1.87m,
            Description = "Test",
            CardNumber = "1234",
            Category = "Test Category",
            Tags = new List<string> { "Test Tag" }
        };
    }
}