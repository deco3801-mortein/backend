using Mortein.Tests.Fixtures;

namespace Mortein.Tests.Controllers.DeviceController;

public partial class DeviceControllerTests(DatabaseContextFixture databaseContextFixture)
    : IClassFixture<DatabaseContextFixture>
{
    private readonly Mortein.Controllers.DeviceController controller = new(databaseContextFixture.databaseContext);
}
