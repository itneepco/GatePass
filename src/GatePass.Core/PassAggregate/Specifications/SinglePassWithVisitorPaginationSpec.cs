using Ardalis.Specification;

namespace GatePass.Core.PassAggregate.Specifications
{
    public class SinglePassWithVisitorPaginationSpec : Specification<SinglePass>
    {
        public SinglePassWithVisitorPaginationSpec(int pageIndex, int pageSize, string searchString)
        {
            Query
                .Include(p => p.Visitor)
                .Where(p =>
                    p.Department.ToLower().Contains(searchString.ToLower()) ||
                    p.Visitor!.FirstName.ToLower().Contains(searchString.ToLower()) ||
                    p.Visitor!.LastName.ToLower().Contains(searchString.ToLower()) ||
                    p.Visitor!.Phone.ToLower().Contains(searchString.ToLower())
                 )
                .OrderByDescending(p => p.VisitDate)
                .Skip(pageIndex * pageSize)
                .Take(pageSize);
        }
    }
}
