using Mortein.Types;
using Mortein.UnitTests.Fixtures;

namespace Mortein.UnitTests.Controllers;

public partial class ControllerTests : IClassFixture<DatabaseContextFixture>
{
    protected readonly DatabaseContext databaseContext;

    public ControllerTests(DatabaseContextFixture databaseContextFixture)
    {
        databaseContext = databaseContextFixture.databaseContext;

        databaseContext.Database.EnsureDeleted();
        databaseContext.Database.EnsureCreated();
    }
}
