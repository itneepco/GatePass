using GatePass.Infrastructure.Data.Seeds;
using Microsoft.Extensions.Logging;

namespace GatePass.Infrastructure.Data;

public class AppDbContextSeed
{
    public static async Task SeedAsync(AppDbContext context, ILoggerFactory loggerFactory)
    {
        try
        {
            await LocationSeed.SeedAsync(context);
            await VisitorSeed.SeedAsync(context);
        }
        catch (Exception exception)
        {
            var log = loggerFactory.CreateLogger<AppDbContextSeed>();
            log.LogError(exception.Message);
            throw;
        }
    }
}
