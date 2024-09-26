using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Mortein.Types;

/// <summary>
/// Healthcheck datum for a device.
/// </summary>
[Table("HealthcheckDatum")]
[PrimaryKey(nameof(DeviceId), nameof(Timestamp))]
public class HealthcheckDatum
{
    /// <summary>
    /// The unique identifier of a device.
    /// </summary>
    [ForeignKey(nameof(Device))]
    public Guid DeviceId { get; set; }

    /// <summary>
    /// The timestamp for this data.
    /// </summary>
    public required DateTime Timestamp { get; set; }

    /// <summary>
    /// The moisture level as a perccentage.
    /// </summary>
    public required float Moisture { get; set; }

    /// <summary>
    /// The measured sunlight level as a perccentage.
    /// </summary>
    public required float Sunlight { get; set; }

    /// <summary>
    /// The temperature in degrees Celcius.
    /// </summary>
    public required float Temperature { get; set; }

    /// <summary>
    /// Whether the device is vibrating at the time of this reading.
    /// </summary>
    public required bool IsVibrating { get; set; }
}
