namespace JsonFileDB.Modules.Volumes;

/// <summary>
/// VolumeRecord model
/// </summary>
public class VolumeRecord
{
    /// <summary>
    /// VolumeRecord name
    /// </summary>
    public string VolumeName { get; set; } = default!;

    /// <summary>
    /// VolumeData list
    /// </summary>
    public List<VolumeData> VolumeData { get; set; } = default!;
}
