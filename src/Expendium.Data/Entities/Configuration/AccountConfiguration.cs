using Expendium.Data.Decorators;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Expendium.Data.Entities.Configuration;

internal sealed class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder) =>
        builder.ApplyAuditableConfiguration();
}