﻿namespace JsonFileDataBase.Services.Tables;

/// <summary>
/// Table handler class
/// </summary>
public class JFDBTables 
{
    /// <summary>
    /// Add a table to a volume.
    /// Templates are MOCK classes that MUST contain 
    /// as many properties as you want to appear in the table to be inserted.
    /// 
    /// Currently it is NECESSARY to create a property Guid Id = Guid.NewGuid()
    /// This acts in such a way that each time that an entry is generated in the table
    /// it automatically generates a unique Guid.
    /// 
    /// The rest of the properties are defined with its type, because in the future it MUST be checked
    /// at the time of making the data insertions 
    /// </summary>
    /// <typeparam name="T">Generic class defining the table to be inserted</typeparam>
    /// <param name="volumen">Table container volume</param>
    /// <param name="table">Table model</param>
    /// <returns>bool</returns>
    internal static bool AddTable<T>(IJFDBVolume volumen, T table) where T : new()
    {
        try
        {
            // Load Json file
            string volumeJson = volumen.Load();

            if (string.IsNullOrEmpty(volumeJson))
                throw new Exception("Volume empty");

            // Instantiate a new volume by deserializing the loaded json
            VolumeRecord? _volumeRecord = JsonConvert.DeserializeObject<VolumeRecord>(volumeJson);

            // Instance a base defining the structure of a volume
            VolumeBase _volumeBase = GetVolumeBase(table);

            // Saves the volume file
            volumen.Save(GetVolumeData(_volumeRecord, _volumeBase));

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return false;
        }
    }

    /// <summary>
    /// Inserts a new entry in a table. 
    /// If there is a property named Id and it is also of type Guid
    /// then it collects the Guid from the table and returns it as a result.
    /// </summary>
    /// <typeparam name="T">Generic class defining the table to be inserted</typeparam>
    /// <param name="volumen">Table container volume</param>
    /// <param name="table">Table model</param>
    /// <returns>Guid</returns>
    /// <exception cref="Exception">Empty volume</exception>
    internal static Guid Insert<T>(IJFDBVolume volumen, T table) where T : new()
    {
        // Load Json file
        string volumeJson = volumen.Load();
        Guid outputGuid = Guid.Empty;

        if (string.IsNullOrEmpty(volumeJson))
            throw new Exception("Volume empty");

        // This makes that all tables MUST have a Guid Id field
        // and it is a dependency that should not exist

        // Gets the type of the table
        Type type = table.GetType();

        // Gets the type of the Id property of the generic instance if exist
        PropertyInfo info = type.GetProperty("Id");  
        if (info is not null && info.PropertyType == typeof(Guid))
        {
            var obj = info.GetValue(table, null);
            outputGuid = Guid.Parse(obj.ToString());
        }

        // Creates an instance of the volume
        VolumeRecord _volumeRecord = JsonConvert.DeserializeObject<VolumeRecord>(volumeJson);

        // Iterates by data volumes
        foreach (var item in _volumeRecord.VolumeData)
        {
            // If the name of the item being iterated matches the name of the item in the table
            // indicates that it is the record that should be modified.
            if (item.Table.Name.Equals(table.ToString()))
            {
                item.Table.Rows.Add(table);
            }
        }

        // Save volume
        volumen.Save(_volumeRecord);

        // Returns the Guid that has been inserted 
        return outputGuid;
    }

    internal static List<TOutput> Get<TOutput,U>(IJFDBVolume volume,string tableColumn, U term, TOutput table) where TOutput : new()
    {
        // Load Json file
        string volumeJson = volume.Load();

        if (string.IsNullOrEmpty(volumeJson))
            throw new Exception("Volume empty");

        // Instantiate an object from the generic class
        TOutput tableInstance = new();
         
        // Creates an instance of the volume
        VolumeRecord _volumeRecord = JsonConvert.DeserializeObject<VolumeRecord>(volumeJson);

        // Get type off table instance
        Type tableInstanceType = tableInstance.GetType();
               
        // Table name to search
        string tableNameToSearch = tableInstanceType.Name;
         
        // Term to search
        string termSearch = term.ToString();

        // Volume selected by table name
        List<VolumeData> volumesByTableName = _volumeRecord.VolumeData.Where(o=> o.Table.Name == tableNameToSearch ).ToList();

        // Rows from volume
        List<object> volumeSelectedRows = volumesByTableName.First().Table.Rows;

        // Result list ouput
        List<TOutput> ouputResultsList = new();

        // Iterate throug rows
        foreach (var selectedRow in volumeSelectedRows)
        { 
            JObject row = (JObject)selectedRow; 
            JToken tokenRow= row.SelectToken($"$..{tableColumn}");

            if (tokenRow is not null && !row.Equals(""))
            {
                if (tokenRow.ToString().Equals(term.ToString()))
                {
                    ouputResultsList.Add(row.ToObject<TOutput>());
                }
            } 
        }

        return ouputResultsList; 
    }

    /// <summary>
    /// Get all rows from table selected
    /// </summary>
    /// <typeparam name="T">Generic class defining the table to be inserted</typeparam>
    /// <param name="volumen">Table container volume</param>
    /// <param name="table">Table model</param>
    /// <returns>List<T></returns>
    /// <exception cref="Exception">Volume empty</exception>
    internal static List<T> GetAll<T>(IJFDBVolume volumen, T table) where T : new()
    {
        // Load Json file
        string volumeJson = volumen.Load();

        if (string.IsNullOrEmpty(volumeJson))
            throw new Exception("Volume empty");

        // Creates an instance of the volume
        VolumeRecord _volumeRecord = JsonConvert.DeserializeObject<VolumeRecord>(volumeJson);

        List<T> list = new();

        foreach (var item in _volumeRecord.VolumeData)
        {

            if (item.Table.Name.Equals(table.ToString()))
            {
                if (item.Table.Rows is not null)
                {
                    foreach (var Row in item.Table.Rows)
                    {
                        list.Add(((JObject)Row).ToObject<T>());
                    }
                }
            }
        }

        return list;
    }

    /// <summary>
    /// Gets a VolumeBase model based on the model in the table
    /// </summary>
    /// <typeparam name="T">Generic class defining the table to be inserted</typeparam>
    /// <param name="table">Table model</param>
    /// <returns>VolumeBase</returns>
    private static VolumeBase GetVolumeBase<T>(T table) where T : new()
    {
        // Instantiate a new volume
        VolumeBase _volumeBase = new();

        // Creates the schema definition of the table to be inserted
        _volumeBase.Schema = GetTableSchemaNames(table);
        _volumeBase.Rows = new();
        _volumeBase.Name = table.ToString();

        // Returns the instance
        return _volumeBase;
    }

    /// <summary>
    /// Gets a list of strings with the names of the properties and fields from the passed table. 
    /// the model of the passed table.
    /// </summary>
    /// <typeparam name="T">Generic class defining the table to be inserted</typeparam>
    /// <param name="table">Table model</param>
    /// <returns>List<string></returns>
    private static List<string> GetTableSchemaNames<T>(T table) where T : new()
    {
        // Gets all the properties of the table class passed as parameter
        Dictionary<string, List<string>> tablePropertiesNames = ConvertToSchema(table);

        // Creates a list and assigns it the following properties
        List<string> propertiesList = new();
        foreach (var property in tablePropertiesNames.Values.First())
        {
            propertiesList.Add(property);
        }

        // Returns the list of properties
        return propertiesList;
    }

    /// <summary>
    /// Get with reflection the properties and fields of the passed model to 
    /// create a dictionary with the key of the model name and its list of strings with the extracted names. 
    /// of strings with the extracted names.
    /// </summary>
    /// <typeparam name="T">Generic class defining the table to be inserted</typeparam>
    /// <param name="table">Table model</param>
    /// <returns>Dictionary<string, List<string>></returns>
    private static Dictionary<string, List<string>> ConvertToSchema<T>(T tables) where T : new()
    {
        // Creates a list to concatenate properties and fields
        List<string> output = new();

        // Creates a dictionary to return the set of properties and fields
        Dictionary<string, List<string>> outputs = new();
        try
        {
            // Instantiate an object from the generic class
            T tableInstance = new();

            // Gets the instance type from the object
            Type tableInstanceType = tableInstance.GetType();

            // Extracts all properties and iterates to add them to the list 
            PropertyInfo[] tablePropertyInfos = tableInstanceType.GetProperties();
            if (tablePropertyInfos.Length > 0)
            {
                foreach (var tablePropertyInfo in tablePropertyInfos)
                {
                    output.Add(tablePropertyInfo.Name);
                }
            }

            // Extracts all fields and iterates to add them to the list
            FieldInfo[] tableFieldInfos = tableInstanceType.GetFields();
            if (tableFieldInfos.Length > 0)
            {
                foreach (var tableFieldInfo in tableFieldInfos)
                {
                    output.Add(tableFieldInfo.Name);
                }
            }

            // Add to the dictionary the name of the table and the list of properties and fields.
            outputs.Add(tableInstance.ToString(), output);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return outputs;

    }

    /// <summary>
    /// Obtains a VolumeRecord model by joining a VolumeRecord and a VolumeBase 
    /// passed in by parameter, setting the new records 
    /// in the VolumeData of the passed VolumeRecord.
    /// </summary>
    /// <param name="volumeRecord">VolumeRecord instance</param>
    /// <param name="volumeBase">VolumeBase instance</param>
    /// <returns>VolumeRecord</returns>
    private static VolumeRecord GetVolumeData(VolumeRecord volumeRecord, VolumeBase volumeBase)
    {
        // Instantiate a new data model for a volume
        VolumeData _volumeData = new();

        // Set the base from the parameters
        _volumeData.Table = volumeBase;

        // To collect the existing data if it exists, otherwise create a new instance 
        volumeRecord.VolumeData = (volumeRecord.VolumeData is null) ? new() : volumeRecord.VolumeData;

        // Adds the data volume to the volume model
        volumeRecord.VolumeData.Add(_volumeData);

        return volumeRecord;
    }
}


