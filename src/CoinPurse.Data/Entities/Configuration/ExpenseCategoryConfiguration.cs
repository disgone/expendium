using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoinPurse.Data.Entities.Configuration;

internal sealed class ExpenseCategoryConfiguration : IEntityTypeConfiguration<ExpenseCategory>
{
    public void Configure(EntityTypeBuilder<ExpenseCategory> builder)
    {
        builder.HasMany(ec => ec.Expenses)
            .WithOne()
            .HasForeignKey(e => e.CategoryId);

        builder.HasData(
            new() { ExpenseCategoryId = 1, Name = "Housing" },
            new() { ExpenseCategoryId = 2, Name = "Utilities" },
            new() { ExpenseCategoryId = 3, Name = "Food" },
            new() { ExpenseCategoryId = 4, Name = "Savings & Investment" },
            new() { ExpenseCategoryId = 5, Name = "Entertainment" },
            new() { ExpenseCategoryId = 6, Name = "Miscellaneous" },
            new() { ExpenseCategoryId = 7, Name = "Transportation" },
            new() { ExpenseCategoryId = 8, Name = "Personal Services" });
    }
}