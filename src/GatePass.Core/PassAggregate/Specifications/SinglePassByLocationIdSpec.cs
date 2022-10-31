using Ardalis.Specification;

namespace GatePass.Core.PassAggregate.Specifications;

public class SinglePassByLocationIdSpec : Specification<SinglePass>
{
	public SinglePassByLocationIdSpec(Guid locationId)
	{
        Query
            .Where(p => p.LocationId == locationId);
    }
}
