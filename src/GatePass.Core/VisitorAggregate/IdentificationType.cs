using Ardalis.SmartEnum;

namespace GatePass.Core.VisitorAggregate;

public class IdentificationType : SmartEnum<IdentificationType>
{
    public static readonly IdentificationType Aadhar = new("Aadhar", 0);
    public static readonly IdentificationType DrivingLicence = new("Driving License", 1);
    public static readonly IdentificationType PanCard = new("PAN Card", 2);
    public static readonly IdentificationType VoterId = new("Voter ID", 3);
    public static readonly IdentificationType Passport = new("Passport", 4);
    public static readonly IdentificationType RationCard = new("Ration Card", 5);
    public static readonly IdentificationType Others = new("Others", 6);

    protected IdentificationType(string name, int value) : base(name, value) { }
}
