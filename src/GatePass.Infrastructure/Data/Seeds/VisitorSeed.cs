using GatePass.Core.VisitorAggregate;

namespace GatePass.Infrastructure.Data.Seeds
{
    public class VisitorSeed
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            if (context.Visitors.Any())
                return;

            var visitors = new List<Visitor>()
            {
                new Visitor(
                    "Tony",
                    "Stark",
                    "9874563210",
                    IdentificationType.Aadhar,
                    "123456",
                    "Bokuloni"
                ),
                new Visitor(
                    "Bruce",
                    "Banner",
                    "9876543210",
                    IdentificationType.Aadhar,
                    "123457",
                    "Duliajan"
                ),
                new Visitor(
                    "Steve",
                    "Rogers",
                    "9876541230",
                    IdentificationType.Aadhar,
                    "123458",
                    "Dibrugarh"
                ),
            };

            context.Visitors.AddRange(visitors);
            await context.SaveChangesAsync();
        }
    }
}
