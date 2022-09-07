using GatePass.Core.VisitorAggregate;
using GatePass.SharedKernel.Interfaces;
using GatePass.SharedKernel;

namespace GatePass.Core.PassAggregate;

public class SinglePass : EntityBase, IAggregateRoot
{
    public DateTime VisitDate { get; set; }
    public DateTime InTime { get; set; }
    public DateTime? OutTime { get; set; }
    public string? Purpose { get; set; }
    public string? OfficerToVisit { get; set; }
    public string? Department { get; set; }
    public int NoOfCompanions { get; set; }
    public Guid VisitorId { get; set; }
    public Visitor? Visitor { get; set; }
}