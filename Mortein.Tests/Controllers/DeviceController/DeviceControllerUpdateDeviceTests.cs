using Microsoft.AspNetCore.Mvc;
using Mortein.Types;

namespace Mortein.Tests.Controllers.DeviceController;

public partial class DeviceControllerTests
{
    [Fact]
    public async void UpdateWithNoDevicesRespondsWith404()
    {
        var result = await controller.UpdateDevice(Guid.Empty, string.Empty);

        var actionResult = Assert.IsType<ActionResult<Device>>(result);
        Assert.IsType<NotFoundResult>(actionResult.Result);
    }
}
