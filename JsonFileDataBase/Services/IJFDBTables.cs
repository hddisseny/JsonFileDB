namespace JsonFileDataBase.Services.Tables
{
    public interface IJFDBTables
    {
        bool AddTable<T>(IJFDBVolume volumen, T table) where T : new();
        List<T> GetAll<T>(IJFDBVolume volumen, T table) where T : new();
        Guid Insert<T>(IJFDBVolume volumen, T table) where T : new();
    }
}