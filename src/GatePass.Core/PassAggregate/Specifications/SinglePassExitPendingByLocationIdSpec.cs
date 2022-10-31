using Ardalis.Specification;

namespace GatePass.Core.PassAggregate.Specifications;

public class SinglePassExitPendingByLocationIdSpec : Specification<SinglePass>
{
	public SinglePassExitPendingByLocationIdSpec(Guid locationId)
	{
        Query
            .Where(p => 
                p.LocationId == locationId && 
                p.VisitDate.Date.Equals(DateTime.Today) &&
                p.OutTime.Equals(TimeSpan.Zero)
            );
    }
}
