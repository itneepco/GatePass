using Ardalis.Specification;

namespace GatePass.Core.PassAggregate.Specifications;

public class SinglePassByLocationIdAndDateSpec : Specification<SinglePass>
{
	public SinglePassByLocationIdAndDateSpec(Guid locationId, DateTime visitDate)
	{
        Query
            .Where(p => 
                p.LocationId == locationId && 
                p.VisitDate.Date.Equals(visitDate.Date)
            );
    }
}
