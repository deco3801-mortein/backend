using Mortein.UnitTests.Fixtures;

namespace Mortein.UnitTests.Controllers.DeviceController;

public partial class DeviceControllerTests : ControllerTests
{
    protected readonly Mortein.Controllers.DeviceController controller;

    public DeviceControllerTests(DatabaseContextFixture databaseContextFixture)
        : base(databaseContextFixture)
    {
        controller = new(databaseContext);
    }
}
