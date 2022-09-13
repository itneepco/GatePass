using Ardalis.Specification;

namespace GatePass.Core.PassAggregate.Specifications
{
    public class MultiPassByVisitorIdPaginationSpec : Specification<MultiplePass>
    {
        public MultiPassByVisitorIdPaginationSpec(int pageIndex, int pageSize, Guid visitorId)
        {
            Query
                .OrderByDescending(p => p.FromDate)
                .Where(p => p.VisitorId == visitorId)
                .Skip(pageIndex * pageSize)
                .Take(pageSize);
        }
    }
}
