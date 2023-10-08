using System.Net;

namespace Ouijjane.Shared.Application.Exceptions;

public class ConfigurationMissingException : CustomException
{
    public ConfigurationMissingException(string sectionName) : base($"{sectionName} Missing in Configurations", HttpStatusCode.NotFound)//TODO: Localisation
    {
    }

    public ConfigurationMissingException(string message, HttpStatusCode statusCode = HttpStatusCode.NotFound) : base(message, statusCode)
    {
    }
}