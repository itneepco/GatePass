using GatePass.Core.LocationAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GatePass.Infrastructure.Data.Config;
public class LocationConfiguration : IEntityTypeConfiguration<Location>
{
  public void Configure(EntityTypeBuilder<Location> builder)
  {
    builder.Property(p => p.Name)
        .HasMaxLength(100)
        .IsRequired();
  }
}
