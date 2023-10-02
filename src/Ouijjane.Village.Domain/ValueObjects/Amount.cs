using Ouijjane.Shared.Domain.ValueObjects;
using Ouijjane.Village.Domain.Enums;

namespace Ouijjane.Village.Domain.ValueObjects
{
    public class Amount : ValueObject
    {
        public Currency Currency { get; set; }
        public decimal Value { get; set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Currency;
            yield return Value;
        }
    }
}
