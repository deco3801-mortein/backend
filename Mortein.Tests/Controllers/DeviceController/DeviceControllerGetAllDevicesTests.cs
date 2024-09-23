using Faker;
using Microsoft.AspNetCore.Mvc;
using Mortein.Types;

namespace Mortein.Tests.Controllers.DeviceController;

[Collection("Sequential")]
public partial class DeviceControllerTests
{
    [Fact]
    public void GetAllWithNoDevices()
    {
        var devices = controller.GetAllDevices();

        Assert.Empty(devices);
    }

    [Fact]
    public async void GetAllGetsOne()
    {
        var createResult = await controller.CreateDevice(Lorem.Sentence());
        var createAction = Assert.IsType<CreatedAtActionResult>(createResult.Result);
        var device = Assert.IsType<Device>(createAction.Value);

        var devices = controller.GetAllDevices();

        Assert.Single(devices);

        Assert.Equal(device, devices.First());

        await controller.DeleteDevice(device.Id);
    }

    [Fact]
    public async void GetAllGetsMany()
    {
        var expectedDevices = new List<Device>();

        for (int i = 0; i < 5; i++)
        {
            var createResult = await controller.CreateDevice(Lorem.Sentence());
            var createAction = Assert.IsType<CreatedAtActionResult>(createResult.Result);
            var device = Assert.IsType<Device>(createAction.Value);
            expectedDevices.Add(device);
        }

        var actualDevices = controller.GetAllDevices();

        Assert.Equal(expectedDevices, actualDevices);

        foreach (var actualDevice in actualDevices)
        {
            await controller.DeleteDevice(actualDevice.Id);
        }

        foreach (var expectedDevice in expectedDevices)
        {
            await controller.DeleteDevice(expectedDevice.Id);
        }
    }
}