using System.Net;

namespace Ouijjane.Shared.Application.Exceptions;
public class ForbiddenException : CustomException
{
    public ForbiddenException() : base("You do not have permissions to access this resource.", HttpStatusCode.Forbidden)//TODO: Localisation
    {
    }
}