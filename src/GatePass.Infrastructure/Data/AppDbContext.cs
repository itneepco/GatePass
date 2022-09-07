using System.Reflection;
using GatePass.Core.LocationAggregate;
using GatePass.Core.PassAggregate;
using GatePass.Core.VisitorAggregate;
using GatePass.SharedKernel;
using GatePass.SharedKernel.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GatePass.Infrastructure.Data;
public class AppDbContext : DbContext
{
    private readonly IDomainEventDispatcher? _dispatcher;

    public AppDbContext(DbContextOptions<AppDbContext> options,
      IDomainEventDispatcher? dispatcher)
        : base(options)
    {
        _dispatcher = dispatcher;
    }

    public DbSet<Location> Locations => Set<Location>();
    public DbSet<SinglePass> SinglePasses => Set<SinglePass>();
    public DbSet<MultiplePass> MultiplePasses => Set<MultiplePass>();
    public DbSet<Visitor> Visitors => Set<Visitor>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        // ignore events if no dispatcher provided
        if (_dispatcher == null) return result;

        // dispatch events only if save was successful
        var entitiesWithEvents = ChangeTracker.Entries<EntityBase>()
            .Select(e => e.Entity)
            .Where(e => e.DomainEvents.Any())
            .ToArray();

        await _dispatcher.DispatchAndClearEvents(entitiesWithEvents);

        return result;
    }

    public override int SaveChanges()
    {
        return SaveChangesAsync().GetAwaiter().GetResult();
    }
}
