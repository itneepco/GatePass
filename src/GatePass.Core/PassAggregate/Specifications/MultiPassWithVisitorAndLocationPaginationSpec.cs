using Ardalis.Specification;

namespace GatePass.Core.PassAggregate.Specifications;

public class MultiPassWithVisitorAndLocationPaginationSpec : Specification<MultiplePass>
{
	public MultiPassWithVisitorAndLocationPaginationSpec(int pageIndex, int pageSize, string searchString)
	{
        Query
            .Include(p => p.Visitor)
            .Include(p => p.Location)
            .Where(p =>
                p.Department.ToLower().Contains(searchString.ToLower()) ||
                p.Visitor!.FirstName.ToLower().Contains(searchString.ToLower()) ||
                p.Visitor!.LastName.ToLower().Contains(searchString.ToLower()) ||
                p.Visitor!.Phone.ToLower().Contains(searchString.ToLower())
            )
            .OrderByDescending(p => p.FromDate)
            .Skip(pageIndex * pageSize)
            .Take(pageSize);
    }
}
