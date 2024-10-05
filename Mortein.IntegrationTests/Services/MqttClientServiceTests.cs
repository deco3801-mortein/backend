using Microsoft.AspNetCore.TestHost;
using Mortein.Mqtt.Services;
using MQTTnet.Client;

namespace Mortein.IntegrationTests.Services;

public class MqttClientServiceTests
{
    private readonly IMqttClient mqttClient;

    private readonly string testTopicName = "test-api";

    public MqttClientServiceTests()
    {

        var _server = new TestServer(new WebHostBuilder().UseStartup<Startup>());

        var mqttClientService = _server.Services.GetRequiredService<MqttClientService>();

        mqttClient = mqttClientService.MqttClient;
    }

    [Fact]
    public async void MqttClientPublishingDoesNotThrow()
    {
        await mqttClient.PublishStringAsync(testTopicName, "Hello from API tests");
    }
}
