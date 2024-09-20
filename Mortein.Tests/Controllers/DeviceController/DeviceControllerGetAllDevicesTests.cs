using Mortein.Types;

namespace Mortein.Tests.Controllers.DeviceController;

public partial class DeviceControllerTests
{
    [Fact]
    public void GetAllWithNoDevices()
    {
        var result = controller.GetAllDevices();

        var devices = Assert.IsAssignableFrom<IEnumerable<Device>>(result);

        Assert.Empty(devices);
    }
}