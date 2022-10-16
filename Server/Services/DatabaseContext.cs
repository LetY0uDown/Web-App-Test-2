using Microsoft.EntityFrameworkCore;
using Models;

namespace Server.Services;

public class DatabaseContext : DbContext
{
    private readonly ConfigurationSettings _configurationSettings;

    public DatabaseContext(ConfigurationSettings configurationSettings)
    {
        _configurationSettings = configurationSettings;

        Database.EnsureCreated();
    }

    public DbSet<Product> Products => Set<Product>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseMySql(_configurationSettings.DBConnectionString,
                                ServerVersion.AutoDetect(_configurationSettings.DBConnectionString));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>().HasKey(p => p.ID);
    }
}