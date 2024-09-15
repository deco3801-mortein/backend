using Microsoft.EntityFrameworkCore;
using Mortein.Types;

namespace Mortein;

/// <inheritdoc/>
public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
{
    /// <summary>
    /// Interface for interacting with devices in the database.
    /// </summary>
    public DbSet<Device> Devices { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(
            $"""
            Host={Environment.GetEnvironmentVariable("POSTGRES_HOST")};
            Username={Environment.GetEnvironmentVariable("POSTGRES_USER")};
            Password={Environment.GetEnvironmentVariable("POSTGRES_PASSWORD")};
            Database={Environment.GetEnvironmentVariable("POSTGRES_DB")};
            """
        );
    }
}
