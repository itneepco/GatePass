using Ardalis.GuardClauses;
using GatePass.SharedKernel;
using GatePass.SharedKernel.Interfaces;

namespace GatePass.Core.LocationAggregate;

public class Location : EntityBase, IAggregateRoot
{
    public string Name { get; private set; } = string.Empty;

    public Location()
    {
    }

    public Location(string name)
    {
        SetName(name);
    }

    public void SetName(string newName)
    {
        Name = Guard.Against.NullOrEmpty(newName, nameof(newName));
    }
}
