using JsonFileDB.Tables;
using JsonFileDB.Volumes;
using System.Reflection;
namespace JsonFileDB.Service;


public class JDB 
{
    private readonly JDBVolumes _volumen;
    public JDB(JDBVolumes volumen)
    {
        _volumen = volumen;
        _volumen.Create();
    }     
    public bool AddTable<T>(T tables) where T : new()
        => JDBTables.AddTable(_volumen.VolumeSource, tables); 
}

 