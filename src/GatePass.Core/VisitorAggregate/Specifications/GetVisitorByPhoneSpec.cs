using Ardalis.Specification;

namespace GatePass.Core.VisitorAggregate.Specifications
{
    public class GetVisitorByPhoneSpec : Specification<Visitor>
    {
        public GetVisitorByPhoneSpec(string phoneNo)
        {
            Query
                .Where(p => p.Phone.Equals(phoneNo));
        }
    }
}
