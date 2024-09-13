using Microsoft.EntityFrameworkCore;

namespace Mortein;

/// <inheritdoc/>
public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
{
    /// <inheritdoc/>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(
            $"""
            Host={Environment.GetEnvironmentVariable("POSTGRES_HOST")};
            Username={Environment.GetEnvironmentVariable("POSTGRES_USER")};
            Password={Environment.GetEnvironmentVariable("POSTGRES_PASSWORD")};
            """
        );
    }
}
