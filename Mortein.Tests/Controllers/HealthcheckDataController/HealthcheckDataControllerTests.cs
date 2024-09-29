using Mortein.Tests.Fixtures;

namespace Mortein.Tests.Controllers.HealthcheckDataController;

public partial class HealthcheckDataControllerTests(DatabaseContextFixture databaseContextFixture)
    : IClassFixture<DatabaseContextFixture>
{
    private readonly Mortein.Controllers.HealthcheckDataController healthcheckDataController = new(databaseContextFixture.databaseContext);
    private readonly Mortein.Controllers.DeviceController deviceController = new(databaseContextFixture.databaseContext);
}
