namespace JsonFileDB.Volumes;

public class VolumeRecord
{
    public string VolumeName { get; set; } = default!;
    public List<VolumeData> VolumeData { get; set; } = default!;
}
