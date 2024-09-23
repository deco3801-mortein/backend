using Faker;
using Microsoft.AspNetCore.Mvc;
using Mortein.Types;

namespace Mortein.Tests.Controllers.DeviceController;

public partial class DeviceControllerTests
{
    [Fact]
    public async Task DeleteWithNoDevicesRespondsWith404Async()
    {
        var result = await controller.DeleteDevice(Guid.NewGuid());

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async void DeleteWithDifferentDeviceRespondsWith404()
    {
        var createResult = await controller.CreateDevice(Lorem.Sentence());
        var createAction = Assert.IsType<CreatedAtActionResult>(createResult.Result);
        var device = Assert.IsType<Device>(createAction.Value);

        var deleteResult = await controller.DeleteDevice(Guid.NewGuid());
        Assert.IsType<NotFoundResult>(deleteResult);

        await controller.DeleteDevice(device.Id);
    }

    [Fact]
    public async void DeleteDeletes()
    {
        var createResult = await controller.CreateDevice(Lorem.Sentence());
        var createAction = Assert.IsType<CreatedAtActionResult>(createResult.Result);
        var device = Assert.IsType<Device>(createAction.Value);

        var deleteResult = await controller.DeleteDevice(device.Id);
        Assert.IsType<NoContentResult>(deleteResult);

        var getResult = await controller.GetDevice(Guid.NewGuid());
        Assert.IsType<NotFoundResult>(getResult.Result);
    }
}
