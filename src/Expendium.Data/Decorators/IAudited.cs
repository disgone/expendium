using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Expendium.Data.Decorators;

public interface IAudited
{
    /// <summary>
    /// The date and time when the record was created.
    /// </summary>
    DateTimeOffset CreatedAt { get; set; }

    /// <summary>
    /// The date and time when the record was last modified.
    /// </summary>
    DateTimeOffset? ModifiedAt { get; set; }
}

public static class ModelBuilderExtensions
{
    public static void ApplyAuditableConfiguration<TEntity>(
        this EntityTypeBuilder<TEntity> builder)
        where TEntity : class, IAudited
    {
        // Configure CreatedAt
        builder.Property(e => e.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP AT TIME ZONE 'UTC'")
            .IsRequired();

        // Configure ModifiedAt
        builder.Property(e => e.ModifiedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP AT TIME ZONE 'UTC'")
            .IsRequired();
    }
}