using Ardalis.Specification;

namespace GatePass.Core.VisitorAggregate.Specifications
{
    public class SearchVisitorByPhoneSpec : Specification<Visitor>
    {
        public SearchVisitorByPhoneSpec(string phoneNo)
        {
            Query
                .Where(p => p.Phone.Contains(phoneNo))
                .Take(10);
        }
    }
}
