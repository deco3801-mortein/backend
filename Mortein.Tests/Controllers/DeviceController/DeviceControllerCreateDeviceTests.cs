using Microsoft.AspNetCore.Mvc;
using Mortein.Types;

namespace Mortein.Tests.Controllers.DeviceController;

public partial class DeviceControllerTests
{
    [Fact]
    public async Task CreateDeviceAddsToDatabaseAsync()
    {
        var existingDevices = controller.GetAllDevices();

        var result = await controller.CreateDevice(string.Empty);

        var actionResult = Assert.IsType<ActionResult<Device>>(result);
        var createdAtAction = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
        var device = Assert.IsType<Device>(createdAtAction.Value);
        Assert.NotNull(device);
        Assert.DoesNotContain(device, existingDevices);

        var resultingDevices = controller.GetAllDevices();
        Assert.Contains(device, resultingDevices);

        await controller.DeleteDevice(device.Id);
    }
}