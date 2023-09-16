using Ouijjane.Shared.Domain.Entities;
using Ouijjane.Village.Domain.ValueObjects;

namespace Ouijjane.Village.Domain.Entities
{
    public class Obligation : AuditableEntity<int>
    {
        public string? Name { get; set; }
        public int Year { get; set; }
        public Amount? Amount { get; set; }
        public bool Paid { get; set; }

        public Inhabitant? Inhabitant { get; set; }
    }
}
