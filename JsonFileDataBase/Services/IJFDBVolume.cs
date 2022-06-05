namespace JsonFileDataBase.Services.Volumes
{
    public interface IJFDBVolume
    {
        string Name { get; set; }
        string? PathJson { get; set; }
        string VolumeSource { get; set; }

        void Create();
        string Load();
        void Save(VolumeRecord recordToParse);
    }
}