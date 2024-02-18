using DotNet.Testcontainers.Containers;
using Expendium.Data.Tests.Core;
using Microsoft.EntityFrameworkCore;
using Testcontainers.MsSql;

namespace Expendium.Data.Mssql.Tests;

public abstract class MssqlDatabaseContainerTest : DatabaseContainerTest
{
    protected override IDatabaseContainer CreateContainer()
    {
        return new MsSqlBuilder()
            .WithImage(@"mcr.microsoft.com/mssql/server:2022-latest")
            .WithPassword("yourStrong(!)Password")
            .Build();
    }

    protected override DbContextOptions<ExpendiumDbContext> DbContextOptions()
    {
        return new DbContextOptionsBuilder<ExpendiumDbContext>()
            .UseSqlServer(ConnectionString)
            .Options;
    }
}