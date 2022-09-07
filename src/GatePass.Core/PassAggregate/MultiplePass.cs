using GatePass.SharedKernel.Interfaces;
using GatePass.Core.VisitorAggregate;
using GatePass.SharedKernel;

namespace GatePass.Core.PassAggregate;

public class MultiplePass : EntityBase, IAggregateRoot
{
    public DateTime FromDate { get; set; }
    public DateTime TillDate { get; set; }
    public string? Purpose { get; set; }
    public string? Department { get; set; }
    public Guid VisitorId { get; set; }
    public Visitor? Visitor { get; set; }
}
