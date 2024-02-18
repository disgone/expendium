using DotNet.Testcontainers.Containers;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Expendium.Data.Tests.Core;

public abstract class DatabaseContainerTest: IAsyncLifetime
{
    protected DatabaseContainerTest()
    {
        SetupContainer();
    }

    protected IDatabaseContainer Container { get; set; } = null!;

    protected string ConnectionString { get; set; } = null!;

    public async Task InitializeAsync()
    {
        if (Container is IContainer container)
        {
            await container.StartAsync();
        }

        ConnectionString = Container.GetConnectionString();
        await using var context = GetDbContext();
        await context.Database.EnsureCreatedAsync();
    }

    public async Task DisposeAsync()
    {
        if (Container is IContainer container)
        {
            await container.StopAsync();
        }
    }

    private void SetupContainer()
    {
        Container = CreateContainer();
    }

    protected abstract IDatabaseContainer CreateContainer();

    protected abstract DbContextOptions<ExpendiumDbContext> DbContextOptions();

    protected virtual ExpendiumDbContext GetDbContext()
    {
        var options = DbContextOptions();
        return new ExpendiumDbContext(options);
    }
}