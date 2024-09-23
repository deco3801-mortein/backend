using Faker;
using Microsoft.AspNetCore.Mvc;
using Mortein.Types;

namespace Mortein.Tests.Controllers.DeviceController;

public partial class DeviceControllerTests
{
    [Fact]
    public async void GetWithNoDevicesRespondsWith404()
    {
        var result = await controller.GetDevice(Guid.NewGuid());

        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async void GetWithDifferentDeviceRespondsWith404()
    {
        var createResult = await controller.CreateDevice(Lorem.Sentence());
        var createAction = Assert.IsType<CreatedAtActionResult>(createResult.Result);
        var device = Assert.IsType<Device>(createAction.Value);

        var getResult = await controller.GetDevice(Guid.NewGuid());

        Assert.IsType<NotFoundResult>(getResult.Result);

        await controller.DeleteDevice(device.Id);
    }

    [Fact]
    public async void GetGets()
    {
        var createResult = await controller.CreateDevice(Lorem.Sentence());
        var createAction = Assert.IsType<CreatedAtActionResult>(createResult.Result);
        var device = Assert.IsType<Device>(createAction.Value);

        var getResult = await controller.GetDevice(device.Id);

        var retrievedDevice = Assert.IsType<Device>(getResult.Value);
        Assert.Equal(device, retrievedDevice);

        await controller.DeleteDevice(device.Id);
    }
}
