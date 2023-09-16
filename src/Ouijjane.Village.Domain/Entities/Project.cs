using Ouijjane.Shared.Domain.Entities;
using Ouijjane.Village.Domain.ValueObjects;

namespace Ouijjane.Village.Domain.Entities
{
    public class Project : AuditableEntity<int>
    {
        public string? Name { get; set; }
        public int Year { get; set; }
        public Amount? Budget { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? AchievementDate { get; set; }
        
        public ICollection<Donation>? Donations { get; set; }
        public ICollection<Work>? Tasks { get; set; }

    }
}
