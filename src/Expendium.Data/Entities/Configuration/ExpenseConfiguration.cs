using CoinPurse.Data.Decorators;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoinPurse.Data.Entities.Configuration;

internal sealed class ExpenseConfiguration : IEntityTypeConfiguration<Expense>
{
    public void Configure(EntityTypeBuilder<Expense> builder)
    {
        builder.ApplyAuditableConfiguration();

        builder.Property(e => e.PaymentFrequency)
            .HasDefaultValue(PaymentFrequency.Monthly)
            .HasConversion<short>();

        builder.HasOne(e => e.Budget)
            .WithMany(e => e.Expenses)
            .HasForeignKey(e => e.BudgetId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<PaymentFrequencyLookup>()
            .WithMany()
            .HasForeignKey(e => e.PaymentFrequency)
            .HasPrincipalKey(e => e.PaymentFrequencyId)
            .IsRequired();

        builder.HasOne<ExpenseCategory>()
            .WithMany(ec => ec.Expenses)
            .HasForeignKey(e => e.CategoryId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}