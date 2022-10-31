using Ardalis.Specification;

namespace GatePass.Core.PassAggregate.Specifications;

public class MultiPassByLocationIdSpec : Specification<MultiplePass>
{
	public MultiPassByLocationIdSpec(Guid locationId)
	{
        Query
            .Where(p => p.LocationId == locationId);
    }
}
