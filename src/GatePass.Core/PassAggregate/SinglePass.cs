using GatePass.Core.VisitorAggregate;
using GatePass.SharedKernel.Interfaces;
using GatePass.SharedKernel;
using Ardalis.GuardClauses;

namespace GatePass.Core.PassAggregate;

public class SinglePass : EntityBase, IAggregateRoot
{
    public DateTime VisitDate { get; private set; }
    public TimeSpan InTime { get; private set; }
    public TimeSpan OutTime { get; private set; }
    public string Purpose { get; private set; } = string.Empty;
    public string OfficerToVisit { get; private set; } = string.Empty;
    public string Department { get; private set; } = string.Empty;
    public int NoOfCompanions { get; private set; }
    public Guid VisitorId { get; private set; }
    public Visitor? Visitor { get; private set; }

    public SinglePass()
    {
    }

    public SinglePass(
        Guid visitorId,
        DateTime visitDate, 
        TimeSpan inTime, 
        string purpose, 
        string officerToVisit, 
        string department,
        int noOfCompanions)
    {
        VisitorId = visitorId;
        SetVisitDate(visitDate);
        SetInTime(inTime);

        SetOfficerToVisit(officerToVisit);
        SetDepartment(department);
        SetPurpose(purpose);
        SetNoOfCompanions(noOfCompanions);
    }

    public void SetOfficerToVisit(string officerToVisit)
    {
        OfficerToVisit = Guard.Against.NullOrEmpty(officerToVisit, nameof(officerToVisit));
    }
    public void SetDepartment(string department)
    {
        Department = Guard.Against.NullOrEmpty(department, nameof(department));
    }

    public void SetPurpose(string purpose)
    {
        Purpose = Guard.Against.NullOrEmpty(purpose, nameof(purpose));
    }

    public void SetNoOfCompanions(int noOfCompanions)
    {
        NoOfCompanions = Guard.Against.Negative(noOfCompanions, nameof(noOfCompanions));
    }

    public void SetVisitDate(DateTime visitDate)
    {
        VisitDate = Guard.Against.Null(visitDate, nameof(visitDate));
    }

    public void SetInTime(TimeSpan inTime)
    {
        InTime = Guard.Against.Null(inTime, nameof(inTime));
    }

    public void SetOutTime(TimeSpan outTime)
    {
        OutTime = Guard.Against.Null(outTime, nameof(outTime));
    }
}