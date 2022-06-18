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
    /// Construct on instanciate workflow, setup inyect IJDBVolumes and IJDBTables instance
    /// </summary>
    /// <param name="volume">Volume config</param>
    public JFDB(IJFDBVolume volume)
    { 
        _volumen = volume;
        _volumen.Create();
    }
    
    /// <summary>
    /// Override constructor with table models list to create
    /// </summary>
    /// <param name="volume">Volume config</param>
    /// <param name="lstTableContext">List with table models</param>
    public JFDB(IJFDBVolume volume, List<object> lstTableContext)
    {
        _volumen = volume;
        _volumen.Create();
        if(lstTableContext is not null)
        {
            lstTableContext.ForEach(t => AddTable(t));
        }
    }

    /// <summary>
    /// Add table to volume
    /// </summary>
    /// <param name="volumen">Table container volume</param>
    /// <param name="table">Table model</param>
    /// <returns>bool</returns>
    public bool AddTable<T>(T tables) where T : new()
        => JFDBTables.AddTable(_volumen, tables);
            
    /// <summary>
    /// Insert a entry in a table 
    /// </summary>
    /// <param name="volumen">Table container volume</param>
    /// <param name="table">Table model</param>
    /// <returns>Guid</returns>
    public Guid Insert<T>(T table) where T : new()
        => JFDBTables.Insert(_volumen, table);

    public List<TOutput> Get<TOutput, U>(string column, U term, TOutput table) where TOutput : new()
    {
        return JFDBTables.Get(_volumen, column, term, table);
    }

    /// <summary>
    /// Get all results from table 
    /// </summary>
    /// <param name="volumen">Table container volume</param>
    /// <param name="table">Table model</param>
    /// <returns></returns>
    public List<T> GetAll<T>(T table) where T : new()
        => JFDBTables.GetAll(_volumen, table);
    
}

 