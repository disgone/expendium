namespace Expendium.Api.Infrastructure;

public abstract class EndpointGroup
{
    /// <summary>
    /// Maps the endpoints for the web application.
    /// </summary>
    /// <param name="app">The web application to map the endpoints to.</param>
    public abstract void Map(WebApplication app);
}