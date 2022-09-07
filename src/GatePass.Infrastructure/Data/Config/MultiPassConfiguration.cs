using GatePass.Core.PassAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GatePass.Infrastructure.Data.Config;
public class MultiPassConfiguration : IEntityTypeConfiguration<MultiplePass>
{
    public void Configure(EntityTypeBuilder<MultiplePass> builder)
    {
        builder.Property(t => t.Department)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(t => t.Purpose)
            .HasMaxLength(150)
            .IsRequired();
    }
}
