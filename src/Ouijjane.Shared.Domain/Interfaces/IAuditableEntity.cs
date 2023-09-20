namespace Ouijjane.Shared.Domain.Interfaces;
public interface IAuditableEntity
{
    DateTime CreatedDateTime { get; set; }

    DateTime? ModifiedDateTime { get; set; }

    int? CreatedByUserId { get; set; }

    int? ModifiedByUserId { get; set; }
}
