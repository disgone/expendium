using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Expendium.Data.Entities;

[Table("PaymentFrequencyTypes")]
public record PaymentFrequencyLookup
{
    [Key]
    public required PaymentFrequency PaymentFrequencyId { get; init; }

    [Required]
    [MaxLength(35)]
    public required string Name { get; init; }
}