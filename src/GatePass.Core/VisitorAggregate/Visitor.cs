using Ardalis.GuardClauses;
using GatePass.SharedKernel;
using GatePass.SharedKernel.Interfaces;

namespace GatePass.Core.VisitorAggregate;

public class Visitor : EntityBase, IAggregateRoot
{
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public string Phone { get; private set; } = string.Empty;
    public string? PhotoName { get; private set; }
    public IdentificationType IdentificationType { get; private set; } = IdentificationType.Others;
    public string IdentificationNo { get; private set; } = string.Empty;
    public string Address { get; private set; } = string.Empty;

    public Visitor()
    {
    }

    public Visitor(
        string firstName, 
        string lastName, 
        string phone, 
        IdentificationType identificationType,
        string identificationNo,
        string address)
    {
        SetFirstName(firstName);
        SetLastName(lastName);
        SetPhone(phone);
        SetAddress(address);
        IdentificationType = identificationType;
        SetIdentificationNo(identificationNo);
    }

    public void SetFirstName(string firstName)
    {
        FirstName = Guard.Against.NullOrEmpty(firstName, nameof(firstName));
    }

    public void SetLastName(string lastName)
    {
        LastName = Guard.Against.NullOrEmpty(lastName, nameof(lastName));
    }

    public void SetPhone(string phone)
    {
        Phone = Guard.Against.NullOrEmpty(phone, nameof(phone));
    }

    public void SetIdentificationType(IdentificationType identificationType)
    {
        IdentificationType = identificationType;
    }

    public void SetIdentificationNo(string identificationNo)
    {
        IdentificationNo = Guard.Against.NullOrEmpty(identificationNo, nameof(identificationNo));
    }

    public void SetAddress(string address)
    {
        Address = Guard.Against.NullOrEmpty(address, nameof(address));
    }

    public void SetPhotoName(string photoName)
    {
        PhotoName = Guard.Against.NullOrEmpty(photoName, nameof(photoName));
    }
}
