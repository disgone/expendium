using CoinPurse.Data.Tests.Core;
using DotNet.Testcontainers.Containers;
using Microsoft.EntityFrameworkCore;
using Testcontainers.MsSql;

namespace CoinPurse.Data.Mssql.Tests;

public abstract class MssqlDatabaseContainerTest : DatabaseContainerTest
{
    protected override IDatabaseContainer CreateContainer()
    {
        return new MsSqlBuilder()
            .WithImage(@"mcr.microsoft.com/mssql/server:2022-latest")
            .WithPassword("yourStrong(!)Password")
            .Build();
    }

    protected override DbContextOptions<CoinDbContext> DbContextOptions()
    {
        return new DbContextOptionsBuilder<CoinDbContext>()
            .UseSqlServer(ConnectionString)
            .Options;
    }
}