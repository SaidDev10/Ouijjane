using System.Net;

namespace Ouijjane.Shared.Application.Exceptions;
public class NotFoundException : CustomException
{
    public NotFoundException(string message) : base(message, HttpStatusCode.NotFound)
    {
    }
}
