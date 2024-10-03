using Faker;
using Microsoft.AspNetCore.Mvc;
using Mortein.Types;

namespace Mortein.UnitTests.Controllers.DeviceController;

public partial class DeviceControllerTests
{
    [Fact]
    public async void UpdateWithNoDevicesRespondsWith404()
    {
        var updateResult = await controller.UpdateDevice(Guid.NewGuid(), Lorem.Sentence());

        Assert.IsType<NotFoundResult>(updateResult.Result);
    }

    [Fact]
    public async void UpdateWithDifferentDeviceRespondsWith404()
    {
        var createResult = await controller.CreateDevice(Lorem.Sentence());
        var createAction = Assert.IsType<CreatedAtActionResult>(createResult.Result);
        var device = Assert.IsType<Device>(createAction.Value);

        var updateResult = await controller.UpdateDevice(Guid.NewGuid(), Lorem.Sentence());

        Assert.IsType<NotFoundResult>(updateResult.Result);

        await controller.DeleteDevice(device.Id);
    }

    [Fact]
    public async void UpdateChangesName()
    {
        var createResult = await controller.CreateDevice(Lorem.Sentence());
        var createAction = Assert.IsType<CreatedAtActionResult>(createResult.Result);
        var device = Assert.IsType<Device>(createAction.Value);

        var newName = Lorem.Sentence();
        var updateResult = await controller.UpdateDevice(device.Id, newName);

        var updatedDevice = Assert.IsType<Device>(updateResult.Value);
        Assert.Equal(device.Id, updatedDevice.Id);
        Assert.Equal(newName, updatedDevice.Name);

        await controller.DeleteDevice(device.Id);
    }
}
