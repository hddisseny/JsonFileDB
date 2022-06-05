namespace JsonFileDB.Modules.Volumes;

/// <summary>
/// VolumenBase model
/// </summary>
public class VolumeBase
{
    /// <summary>
    /// VolumeBase name
    /// </summary>
    public string Name { get; set; } = default!;

    /// <summary>
    /// VolumeBase schema list
    /// </summary>
    public List<string> Schema { get; set; } = default!;

    /// <summary>
    /// VolumeBase rows list
    /// </summary>
    public List<object> Rows { get; set; } = default!;
}
