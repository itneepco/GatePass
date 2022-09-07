using GatePass.Core.PassAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GatePass.Infrastructure.Data.Config;

public class SinglePassConfiguration : IEntityTypeConfiguration<SinglePass>
{
    public void Configure(EntityTypeBuilder<SinglePass> builder)
    {
        builder.Property(t => t.OfficerToVisit)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(t => t.Department)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(t => t.Purpose)
            .HasMaxLength(150)
            .IsRequired();
    }
}
