using Ouijjane.Shared.Domain.Entities;
using Ouijjane.Village.Domain.ValueObjects;

namespace Ouijjane.Village.Domain.Entities
{
    public class Donation : AuditableEntity<int>
    {
        public Amount? Amount { get; set; }
        public bool Paid { get; set; } = false;
        public DateOnly? PaidDate { get; set; }

        public virtual Donor? Donor { get; set; }
        public virtual Project? Project { get; set; }
    }
}
