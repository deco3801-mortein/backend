using Microsoft.AspNetCore.Mvc;
using Mortein.Types;

namespace Mortein.Tests.Controllers.DeviceController;

public partial class DeviceControllerTests
{
    [Fact]
    public async Task DeleteWithNoDevicesRespondsWith404Async()
    {
        var result = await controller.DeleteDevice(Guid.Empty);

        var actionResult = Assert.IsAssignableFrom<IActionResult>(result);
        Assert.IsType<NotFoundResult>(actionResult);
    }
}