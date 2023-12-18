using CoinPurse.Api.Infrastructure;

namespace CoinPurse.Api.Endpoints.Accounts;

public class Accounts : EndpointGroup
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this);
    }
}