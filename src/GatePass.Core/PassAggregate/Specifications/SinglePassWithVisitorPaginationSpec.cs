using Ardalis.Specification;

namespace GatePass.Core.PassAggregate.Specifications
{
    public class SinglePassWithVisitorPaginationSpec : Specification<SinglePass>
    {
        public SinglePassWithVisitorPaginationSpec(int pageIndex, int pageSize, string searchString)
        {
            Query
                .Where(p =>
                    p.Department.ToLower().Contains(searchString.ToLower()) ||
                    p.OfficerToVisit.ToLower().Contains(searchString.ToLower())
                 )
                .Include(p => p.Visitor)
                .OrderByDescending(p => p.VisitDate)
                .Skip(pageIndex * pageSize)
                .Take(pageSize);
        }
    }
}
