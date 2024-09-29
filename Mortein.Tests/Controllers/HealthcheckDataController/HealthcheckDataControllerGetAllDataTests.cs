using Faker;
using Microsoft.AspNetCore.Mvc;
using Mortein.Types;
using NodaTime;

namespace Mortein.Tests.Controllers.HealthcheckDataController;

[Collection("Sequential")]
public partial class HealthcheckDataControllerTests
{
    [Fact]
    public void GetAllDataWithNoDevices()
    {
        var healthcheckData = healthcheckDataController.GetAllHealthcheckDataForDevice(Guid.NewGuid());

        Assert.Empty(healthcheckData);
    }

    [Fact]
    public async void GetAllDataWithDifferentDevice()
    {
        var createResult = await deviceController.CreateDevice(Lorem.Sentence());
        var createAction = Assert.IsType<CreatedAtActionResult>(createResult.Result);
        var device = Assert.IsType<Device>(createAction.Value);

        var data = healthcheckDataController.GetAllHealthcheckDataForDevice(Guid.NewGuid());

        Assert.Empty(data);

        await deviceController.DeleteDevice(device.Id);
    }

    [Fact]
    public async void GetAllDataGetsOne()
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

        databaseContextFixture.databaseContext.HealthcheckData.Add(healthcheckDatum);
        databaseContextFixture.databaseContext.SaveChanges();

        var data = healthcheckDataController.GetAllHealthcheckDataForDevice(device.Id);

        var datum = Assert.Single(data);
        Assert.Equal(datum, healthcheckDatum);

        databaseContextFixture.databaseContext.HealthcheckData.Remove(healthcheckDatum);
        databaseContextFixture.databaseContext.SaveChanges();

        await deviceController.DeleteDevice(device.Id);
    }

    [Fact]
    public async void GetAllGetsMany()
    {
        var createResult = await deviceController.CreateDevice(Lorem.Sentence());
        var createAction = Assert.IsType<CreatedAtActionResult>(createResult.Result);
        var device = Assert.IsType<Device>(createAction.Value);


        var expectedData = new List<HealthcheckDatum>();

        for (int i = 0; i < 5; i++)
        {
            HealthcheckDatum healthcheckDatum = new()
            {
                DeviceId = device.Id,
                Timestamp = new Instant() + Duration.FromHours(i),
                Moisture = RandomNumber.Next(100) / 100,
                Sunlight = RandomNumber.Next(100) / 100,
                Temperature = RandomNumber.Next(50),
                IsVibrating = Faker.Boolean.Random(),
            };

            databaseContextFixture.databaseContext.HealthcheckData.Add(healthcheckDatum);
            expectedData.Add(healthcheckDatum);
        }

        expectedData.Reverse();

        databaseContextFixture.databaseContext.SaveChanges();

        var actualData = healthcheckDataController.GetAllHealthcheckDataForDevice(device.Id);

        Assert.Equal(expectedData, actualData);

        foreach (var healthcheckDatum in expectedData)
        {
            databaseContextFixture.databaseContext.HealthcheckData.Remove(healthcheckDatum);
        }

        databaseContextFixture.databaseContext.SaveChanges();

        await deviceController.DeleteDevice(device.Id);
    }
}
