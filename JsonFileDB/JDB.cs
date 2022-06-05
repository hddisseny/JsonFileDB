using JsonFileDB.Tables;
using JsonFileDB.Volumes; 
namespace JsonFileDB.Service;


public class JDB 
{
    private readonly JDBVolumes _volumen;

    public JDB(JDBVolumes volumen) 
        => _volumen = volumen; 

    public void Create() 
        => _volumen.Create();

    public bool AddTable<T>(T tables) where T : new()
        => JDBTables.AddTable(_volumen, tables); 

    public Guid Insert<T>(T table) where T : new()
        => JDBTables.Insert(_volumen, table);
}

 