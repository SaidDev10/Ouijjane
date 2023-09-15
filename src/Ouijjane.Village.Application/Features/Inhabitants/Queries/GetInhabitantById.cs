namespace Ouijjane.Village.Application.Features.Inhabitants.Queries;
public class GetInhabitantByIdResponse
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? FatherName { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public string Birthdate { get; set; }
    public bool IsMarried { get; set; }
}
