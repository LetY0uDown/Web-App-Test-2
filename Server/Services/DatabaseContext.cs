using Microsoft.EntityFrameworkCore;
using Models;

namespace Server.Services;

public class DatabaseContext : DbContext
{
    private readonly ConfigurationSettings _config;

    public DatabaseContext(ConfigurationSettings config)
    {
        _config = config;

        Database.EnsureCreated();
    }

    public DbSet<Product> Products => Set<Product>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseMySql(_config.DBConnectionString,
                                ServerVersion.AutoDetect(_config.DBConnectionString));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>().HasKey(p => p.ID);
    }
}