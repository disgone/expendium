using CoinPurse.Data.Entities;
using CoinPurse.Data.Entities.Configuration;
using CoinPurse.Data.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace CoinPurse.Data;

public class CoinDbContext : DbContext
{
    public CoinDbContext()
    {
    }

    public CoinDbContext(DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<Expense> Expenses { get; set; } = null!;
    public DbSet<Account> Accounts { get; set; } = null!;
    public DbSet<Budget> Budgets { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ExpenseConfiguration).Assembly);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.AddInterceptors(AuditDateChangeInterceptor.Instance);
    }
}