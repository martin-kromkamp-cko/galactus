using System.Data.Common;

namespace Processing.Configuration;

public class EntityBase
{
    internal EntityBase() { }

    internal EntityBase(string externalId)
    {
        ExternalId = externalId;
        CreatedOn = DateTimeOffset.UtcNow;
        UpdatedOn = DateTimeOffset.UtcNow;
        IsActive = true;
    }

    public long? Id { get; private set; }

    public string ExternalId { get; private set; }

    public DateTimeOffset CreatedOn { get; private set; }

    public DateTimeOffset UpdatedOn { get; protected set; }

    public bool IsActive { get; private set; }

    public void ToggleActive()
    {
        IsActive = !IsActive;
        UpdatedOn = DateTimeOffset.UtcNow;
    }
}
