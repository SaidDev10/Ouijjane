using Ouijjane.Shared.Domain.Interfaces;

namespace Ouijjane.Shared.Domain.Entities;
public abstract class AuditableEntity : DomainEntity, IAuditableEntity
{
    public DateTime CreatedDateTime { get; set; }

    public DateTime? ModifiedDateTime { get; set; }

    public int? CreatedByUserId { get; set; }

    public int? ModifiedByUserId { get; set; }
}

public abstract class AuditableEntity<TKey> : DomainEntity<TKey>, IAuditableEntity
{
    public DateTime CreatedDateTime { get; set; }

    public DateTime? ModifiedDateTime { get; set; }

    public int? CreatedByUserId { get; set; }

    public int? ModifiedByUserId { get; set; }
}
