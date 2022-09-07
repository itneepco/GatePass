using GatePass.Core.LocationAggregate;

namespace GatePass.Infrastructure.Data.Seeds
{
    public class LocationSeed
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            if (context.Locations.Any())
                return;

            var locations = new List<Location>()
            {
                new Location("AGBPS"),
                new Location("Shillong"),
                new Location("Guwahati"),
            };

            context.Locations.AddRange(locations);
            await context.SaveChangesAsync();
        }
    }
}
