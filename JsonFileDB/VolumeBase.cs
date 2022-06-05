namespace JsonFileDB.Volumes;

public class VolumeBase
{
    public string Name { get; set; } = default!;
    public List<string> Schema { get; set; } = default!;
    public List<object> Rows { get; set; } = default!;
}
