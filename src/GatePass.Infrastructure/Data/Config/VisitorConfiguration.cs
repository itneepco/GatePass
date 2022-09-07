using GatePass.Core.VisitorAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GatePass.Infrastructure.Data.Config;
public class VisitorConfiguration : IEntityTypeConfiguration<Visitor>
{
    public void Configure(EntityTypeBuilder<Visitor> builder)
    {
        builder.Property(t => t.FirstName)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(t => t.LastName)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(t => t.IdentificationType)
            .HasConversion(
                p => p.Value,
                p => IdentificationType.FromValue(p));

        builder.Property(t => t.IdentificationNo)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(t => t.Address)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(t => t.Phone)
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(t => t.PhotoName)
            .HasMaxLength(50);
    }
}
