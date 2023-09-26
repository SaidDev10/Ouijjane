namespace Ouijjane.Village.Domain.Entities
{
    public class Inhabitant : Donor
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string FatherName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public DateOnly Birthdate { get; set; }
        public bool IsMarried { get; set; }
    }
}
