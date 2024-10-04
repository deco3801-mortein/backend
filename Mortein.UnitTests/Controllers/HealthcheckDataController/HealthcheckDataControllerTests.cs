using Mortein.UnitTests.Fixtures;

namespace Mortein.UnitTests.Controllers.HealthcheckDataController;

public partial class HealthcheckDataControllerTests : ControllerTests
{
    private readonly Mortein.Controllers.HealthcheckDataController healthcheckDataController;
    private readonly Mortein.Controllers.DeviceController deviceController;

    public HealthcheckDataControllerTests(DatabaseContextFixture databaseContextFixture)
        : base(databaseContextFixture)
    {
        healthcheckDataController = new(databaseContext);
        deviceController = new(databaseContext);
    }
}
