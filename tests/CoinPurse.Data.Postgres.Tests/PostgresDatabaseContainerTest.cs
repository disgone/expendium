using CoinPurse.Data.Tests.Core;
using DotNet.Testcontainers.Containers;
using Microsoft.EntityFrameworkCore;
using Testcontainers.PostgreSql;

namespace CoinPurse.Data.Postgres.Tests;

public abstract class PostgresDatabaseContainerTest: DatabaseContainerTest
{
    protected override IDatabaseContainer CreateContainer()
    {
        return new PostgreSqlBuilder()
            .WithImage("postgres:16.1-bookworm")
            .WithPassword("yourStrong(!)Password")
            .Build();
    }

    protected override DbContextOptions<CoinDbContext> DbContextOptions()
    {
        return new DbContextOptionsBuilder<CoinDbContext>()
            .UseNpgsql(ConnectionString)
            .Options;
    }
}