using Microsoft.EntityFrameworkCore;

namespace Mortein.Types;

/// <inheritdoc/>
public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
{
    /// <summary>
    /// Interface for interacting with devices in the database.
    /// </summary>
    public DbSet<Device> Devices { get; set; }

    /// <summary>
    /// Interface for interacting with healthcheck data in the database.
    /// </summary>
    public DbSet<HealthcheckDatum> HealthcheckData { get; set; }

    /// <inheritdoc/>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(
            $"""
            Host={Environment.GetEnvironmentVariable("POSTGRES_HOST")};
            Username={Environment.GetEnvironmentVariable("POSTGRES_USER")};
            Password={Environment.GetEnvironmentVariable("POSTGRES_PASSWORD")};
            Database={Environment.GetEnvironmentVariable("POSTGRES_DB")};
            """,
            options => options.UseNodaTime()
        );
    }
}
