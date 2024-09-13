using System.ComponentModel.DataAnnotations.Schema;

namespace Mortein.Models;

/// <summary>
/// Pest-deterrent device.
/// </summary>
[Table("Device")]
public class Device
{
    /// <summary>
    /// The unique identifier of a device.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The name of a device.
    /// </summary>
    public required string Name { get; set; }
}
