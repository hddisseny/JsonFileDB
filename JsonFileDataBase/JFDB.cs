
namespace JsonFileDataBase;

/// <summary>
/// Main workflow class
/// </summary>
public class JFDB  
{
    /// <summary>
    /// IJFDBVolume inyect instance
    /// </summary>
    private readonly IJFDBVolume _volumen;

    /// <summary>
    /// IJFDBTables inyect instance
    /// </summary>
    private readonly IJFDBTables _table;

    /// <summary>
    /// Construct on instanciate workflow, setup inyect IJDBVolumes and IJDBTables instance
    /// </summary>
    /// <param name="volumen">Volumen config</param>
    public JFDB(IJFDBVolume volumen, IJFDBTables table)
    { 
        _volumen = volumen;
        _table = table;
    }
        
    /// <summary>
    /// Create volume
    /// </summary>
    public void CreateVolume() 
        =>  _volumen.Create();
     
    /// <summary>
    /// Add table to volume
    /// </summary>
    /// <param name="volumen">Table container volume</param>
    /// <param name="table">Table model</param>
    /// <returns>bool</returns>
    public bool AddTable<T>(T tables) where T : new()
        => _table.AddTable(_volumen, tables);
            
    /// <summary>
    /// Insert a entry in a table 
    /// </summary>
    /// <param name="volumen">Table container volume</param>
    /// <param name="table">Table model</param>
    /// <returns>Guid</returns>
    public Guid Insert<T>(T table) where T : new()
        => _table.Insert(_volumen, table);

    /// <summary>
    /// Get all results from table 
    /// </summary>
    /// <param name="volumen">Table container volume</param>
    /// <param name="table">Table model</param>
    /// <returns></returns>
    public List<T> GetAll<T>(T table) where T : new()
        => _table.GetAll(_volumen, table);
    
}

 