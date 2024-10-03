using Faker;
using Microsoft.AspNetCore.Mvc;
using Mortein.Types;

namespace Mortein.UnitTests.Controllers.DeviceController;

public partial class DeviceControllerTests
{
    [Fact]
    public async void CreateDeviceAddsToDatabase()
    {
        var existingDevices = controller.GetAllDevices();
        var result = await controller.CreateDevice(Lorem.Sentence());
        var resultingDevices = controller.GetAllDevices();

        var createdAtAction = Assert.IsType<CreatedAtActionResult>(result.Result);
        var device = Assert.IsType<Device>(createdAtAction.Value);

        Assert.NotNull(device);
        Assert.DoesNotContain(device, existingDevices);
        Assert.Contains(device, resultingDevices);

        await controller.DeleteDevice(device.Id);
    }
}
