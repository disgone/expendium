using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Expendium.Data.Entities.Configuration;

internal sealed class
    ExpenseCategoryConfiguration : IEntityTypeConfiguration<ExpenseCategory>
{
    public void Configure(EntityTypeBuilder<ExpenseCategory> builder)
    {
        builder.HasIndex(n => n.Name)
            .IsUnique();

        builder.HasMany(ec => ec.Expenses)
            .WithOne()
            .HasForeignKey(e => e.CategoryId);

        builder.HasData(
            new ExpenseCategory { ExpenseCategoryId = 1, Name = "Housing" },
            new ExpenseCategory { ExpenseCategoryId = 2, Name = "Utilities" },
            new ExpenseCategory { ExpenseCategoryId = 3, Name = "Food" },
            new ExpenseCategory { ExpenseCategoryId = 4, Name = "Savings & Investment" },
            new ExpenseCategory { ExpenseCategoryId = 5, Name = "Entertainment" },
            new ExpenseCategory { ExpenseCategoryId = 6, Name = "Miscellaneous" },
            new ExpenseCategory { ExpenseCategoryId = 7, Name = "Transportation" },
            new ExpenseCategory { ExpenseCategoryId = 8, Name = "Personal Services" });
    }
}