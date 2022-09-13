using GatePass.SharedKernel.Interfaces;
using GatePass.Core.VisitorAggregate;
using GatePass.SharedKernel;
using Ardalis.GuardClauses;

namespace GatePass.Core.PassAggregate;

public class MultiplePass : EntityBase, IAggregateRoot
{
    public DateTime FromDate { get; private set; }
    public DateTime TillDate { get; private set; }
    public string Purpose { get; private set; } = string.Empty;
    public string Department { get; private set; } = string.Empty;
    public Guid VisitorId { get; private set; }
    public Visitor? Visitor { get; private set; }

    public MultiplePass()
    {
    }

    public MultiplePass(
        Guid visitorId,
        DateTime fromDate,
        DateTime tillDate,
        string purpose,
        string department
    )
    {
        VisitorId = visitorId;

        SetFromDate(fromDate);
        SetTillDate(tillDate);
        SetPurpose(purpose);
        SetDepartment(department);   
    }

    public void SetFromDate(DateTime fromDate)
    {
        FromDate = Guard.Against.Null(fromDate, nameof(fromDate));    
    }

    public void SetTillDate(DateTime tillDate)
    {
        TillDate = Guard.Against.Null(tillDate, nameof(tillDate));
    }

    public void SetDepartment(string department)
    {
        Department = Guard.Against.Null(department, nameof(department));
    }

    public void SetPurpose(string purpose)
    {
        Purpose = Guard.Against.NullOrEmpty(purpose, nameof(purpose));
    }
}
