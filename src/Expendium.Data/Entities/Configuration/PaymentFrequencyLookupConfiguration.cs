using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Expendium.Data.Entities.Configuration;

internal sealed class
    PaymentFrequencyLookupConfiguration : IEntityTypeConfiguration<PaymentFrequencyLookup>
{
    public void Configure(EntityTypeBuilder<PaymentFrequencyLookup> builder) =>
        builder.HasData(
            Enum.GetValues(typeof(PaymentFrequency))
                .Cast<PaymentFrequency>()
                .Select(pf =>
                    new PaymentFrequencyLookup
                    {
                        PaymentFrequencyId = pf, Name = pf.ToString()
                    })
        );
}