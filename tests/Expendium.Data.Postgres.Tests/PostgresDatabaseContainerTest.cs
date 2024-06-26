using DotNet.Testcontainers.Containers;
using Expendium.Data.Tests.Core;
using Microsoft.EntityFrameworkCore;
using Testcontainers.PostgreSql;

namespace Expendium.Data.Postgres.Tests;

public abstract class PostgresDatabaseContainerTest : DatabaseContainerTest
{
    protected override IDatabaseContainer CreateContainer() =>
        new PostgreSqlBuilder()
            .WithImage("postgres:16.2-bookworm")
            .WithPassword("yourStrong(!)Password")
            .Build();

    protected override DbContextOptions<ExpendiumDbContext> DbContextOptions() =>
        new DbContextOptionsBuilder<ExpendiumDbContext>()
            .UseNpgsql(ConnectionString)
            .Options;
}