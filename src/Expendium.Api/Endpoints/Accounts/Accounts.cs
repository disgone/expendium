using Expendium.Api.Infrastructure;

namespace Expendium.Api.Endpoints.Accounts;

public class Accounts : EndpointGroup
{
    public override void Map(WebApplication app) => app.MapGroup(this);
}