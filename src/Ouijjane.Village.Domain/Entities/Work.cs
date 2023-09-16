using Ouijjane.Shared.Domain.Entities;
using Ouijjane.Village.Domain.ValueObjects;

namespace Ouijjane.Village.Domain.Entities
{
    public class Work : AuditableEntity<int>
    {
        public string? Name { get; set; }
        public Amount? Budget { get; set; }

        public Project? Project { get; set; }
    }
}
