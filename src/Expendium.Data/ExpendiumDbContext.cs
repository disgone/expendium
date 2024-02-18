using Expendium.Data.Entities;
using Expendium.Data.Entities.Configuration;
using Expendium.Data.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace Expendium.Data;

public class ExpendiumDbContext : DbContext
{
    public ExpendiumDbContext()
    {
    }

    public ExpendiumDbContext(DbContextOptions options)
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