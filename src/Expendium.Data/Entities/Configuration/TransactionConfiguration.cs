using Expendium.Data.Decorators;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Expendium.Data.Entities.Configuration;

internal sealed class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ApplyAuditableConfiguration();

        builder.Property(n => n.Status)
            .HasDefaultValue(TransactionStatus.Imported)
            .HasConversion<short>();

        builder.HasIndex(n => n.Status);

        builder.HasIndex(n => n.Category);

        builder.HasIndex(n => n.TransactionSignature);

        builder.HasIndex(n => n.SourceAccount);
    }
}