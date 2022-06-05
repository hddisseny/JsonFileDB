using JsonFileDB.Tables;
using JsonFileDB.Volumes; 

namespace JsonFileDB.Service;

/// <summary>
/// Main workflow class
/// </summary>
public class JDB 
{
    /// <summary>
    /// JDBVolumes instance
    /// </summary>
    private readonly JDBVolumes _volumen;

    /// <summary>
    /// Construct on instanciate workflow, setup private JDBVolumes instance
    /// </summary>
    /// <param name="volumen">Volumen config</param>
    public JDB(JDBVolumes volumen) 
        => _volumen = volumen; 

    /// <summary>
    /// Create volume
    /// </summary>
    public void CreateVolume() 
        => _volumen.Create();

    /// <summary>
    /// Add table to volume
    /// </summary>
    /// <param name="volumen">Table container volume</param>
    /// <param name="table">Table model</param>
    /// <returns>bool</returns>
    public bool AddTable<T>(T tables) where T : new()
        => JDBTables.AddTable(_volumen, tables);

    /// <summary>
    /// Insert a entry in a table 
    /// </summary>
    /// <param name="volumen">Table container volume</param>
    /// <param name="table">Table model</param>
    /// <returns>Guid</returns>
    public Guid Insert<T>(T table) where T : new()
        => JDBTables.Insert(_volumen, table);
}

 