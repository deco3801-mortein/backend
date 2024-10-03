using Microsoft.EntityFrameworkCore;
using Mortein.Types;

namespace Mortein.UnitTests.Fixtures;

public class DatabaseContextFixture : IDisposable
{
    public DatabaseContext databaseContext;

    public DatabaseContextFixture()
    {
        DbContextOptions<DatabaseContext> options = new DbContextOptionsBuilder<DatabaseContext>().Options;
        databaseContext = new(options);

        databaseContext.Database.EnsureCreated();
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}
