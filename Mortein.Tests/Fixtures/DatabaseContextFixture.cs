using Microsoft.EntityFrameworkCore;

namespace Mortein.Tests.Fixtures;

public class DatabaseContextFixture : IDisposable
{
    public DatabaseContext databaseContext;

    public DatabaseContextFixture()
    {
        DbContextOptions<DatabaseContext> options = new DbContextOptionsBuilder<DatabaseContext>().Options;
        databaseContext = new(options);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}
