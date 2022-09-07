using Ardalis.Specification;

namespace GatePass.Core.VisitorAggregate
{
    public class VisitorsPaginationSpec : Specification<Visitor>
    {
        public VisitorsPaginationSpec(int pageIndex, int pageSize, string searchString)
        {
            Query
                .Where(p => 
                    p.FirstName.ToLower().Contains(searchString.ToLower()) ||
                    p.LastName.ToLower().Contains(searchString.ToLower()) ||
                    p.Phone.ToLower().Contains(searchString.ToLower())
                 )
                .Skip(pageIndex* pageSize)
                .Take(pageSize);
        }
    }
}
