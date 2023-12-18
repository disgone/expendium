using CoinPurse.Data;
using Microsoft.EntityFrameworkCore;

namespace DataScaffold;

public class MssqlCoinDbContext : CoinDbContext
{
    public MssqlCoinDbContext()
    {
    }

    public MssqlCoinDbContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        if(!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=CoinPurse;User Id=sa;Password=yourStrong(!)Password;TrustServerCertificate=True;");
        }
    }
}
