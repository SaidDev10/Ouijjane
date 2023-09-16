namespace Ouijjane.Shared.Domain.Entities;
public abstract class AuditableEntity : DomainEntity
{
    public DateTime Created { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? LastModified { get; set; }

    public string? LastModifiedBy { get; set; }
}

public abstract class AuditableEntity<TKey> : DomainEntity<TKey>
{
    public DateTime Created { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? LastModified { get; set; }

    public string? LastModifiedBy { get; set; }
}
