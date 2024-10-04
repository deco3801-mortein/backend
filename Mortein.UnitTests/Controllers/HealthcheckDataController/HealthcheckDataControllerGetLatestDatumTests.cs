using Faker;
using Microsoft.AspNetCore.Mvc;
using Mortein.Types;
using NodaTime;

namespace Mortein.UnitTests.Controllers.HealthcheckDataController;

public partial class HealthcheckDataControllerTests
{
    [Fact]
    public void GetLatestDatumWithNoDevicesRespondsWith404()
    {
        var result = healthcheckDataController.GetLatestHealthcheckDatumForDevice(Guid.NewGuid());

        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async void GetLatestDatumWithDifferentDeviceRespondsWith404()
    {
        var createResult = await deviceController.CreateDevice(Lorem.Sentence());
        var createAction = Assert.IsType<CreatedAtActionResult>(createResult.Result);
        var device = Assert.IsType<Device>(createAction.Value);

        var getResult = healthcheckDataController.GetLatestHealthcheckDatumForDevice(Guid.NewGuid());

        Assert.IsType<NotFoundResult>(getResult.Result);

        await deviceController.DeleteDevice(device.Id);
    }

    [Fact]
    public async void GetLatestDatumWithNoDatumDeviceRespondsWith404()
    {
        var createResult = await deviceController.CreateDevice(Lorem.Sentence());
        var createAction = Assert.IsType<CreatedAtActionResult>(createResult.Result);
        var device = Assert.IsType<Device>(createAction.Value);

        var getResult = healthcheckDataController.GetLatestHealthcheckDatumForDevice(Guid.NewGuid());

        Assert.IsType<NotFoundResult>(getResult.Result);

        await deviceController.DeleteDevice(device.Id);
    }

    [Fact]
    public async void GetLatestDatumGetsOneDatum()
    {
        var createResult = await deviceController.CreateDevice(Lorem.Sentence());
        var createAction = Assert.IsType<CreatedAtActionResult>(createResult.Result);
        var device = Assert.IsType<Device>(createAction.Value);

        HealthcheckDatum healthcheckDatum = new()
        {
            DeviceId = device.Id,
            Timestamp = new Instant(),
            Moisture = RandomNumber.Next(100) / 100,
            Sunlight = RandomNumber.Next(100) / 100,
            Temperature = RandomNumber.Next(50),
            IsVibrating = Faker.Boolean.Random(),
        };

        databaseContext.HealthcheckData.Add(healthcheckDatum);
        databaseContext.SaveChanges();

        var getResult = healthcheckDataController.GetLatestHealthcheckDatumForDevice(device.Id);

        var retrievedHealthcheckDatum = Assert.IsType<HealthcheckDatum>(getResult.Value);
        Assert.Equal(healthcheckDatum, retrievedHealthcheckDatum);

        databaseContext.Remove(healthcheckDatum);
        databaseContext.SaveChanges();

        await deviceController.DeleteDevice(device.Id);
    }

    [Fact]
    public async void GetLatestDatumGetsLatestDatum()
    {
        var createResult = await deviceController.CreateDevice(Lorem.Sentence());
        var createAction = Assert.IsType<CreatedAtActionResult>(createResult.Result);
        var device = Assert.IsType<Device>(createAction.Value);

        HealthcheckDatum healthcheckDatum = new()
        {
            DeviceId = device.Id,
            Timestamp = new Instant(),
            Moisture = RandomNumber.Next(100) / 100,
            Sunlight = RandomNumber.Next(100) / 100,
            Temperature = RandomNumber.Next(50),
            IsVibrating = Faker.Boolean.Random(),
        };

        HealthcheckDatum latestHealthcheckDatum = new()
        {
            DeviceId = device.Id,
            Timestamp = healthcheckDatum.Timestamp + Duration.FromHours(1),
            Moisture = RandomNumber.Next(100) / 100,
            Sunlight = RandomNumber.Next(100) / 100,
            Temperature = RandomNumber.Next(50),
            IsVibrating = Faker.Boolean.Random(),
        };

        databaseContext.HealthcheckData.AddRange(healthcheckDatum, latestHealthcheckDatum);
        databaseContext.SaveChanges();

        var getResult = healthcheckDataController.GetLatestHealthcheckDatumForDevice(device.Id);

        var retrievedHealthcheckDatum = Assert.IsType<HealthcheckDatum>(getResult.Value);
        Assert.Equal(latestHealthcheckDatum, retrievedHealthcheckDatum);

        databaseContext.RemoveRange(healthcheckDatum, latestHealthcheckDatum);
        databaseContext.SaveChanges();

        await deviceController.DeleteDevice(device.Id);
    }
}
