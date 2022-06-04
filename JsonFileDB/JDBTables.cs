using JsonFileDB.Volumes;
using Newtonsoft.Json; 
using System.Reflection;
using System.Text; 

namespace JsonFileDB.Tables;

public class JDBTables
{

    public static bool AddTable<T>(string volumenSource, T table) where T : new()
    {

        try
        {
            string volumeJson = File.ReadAllText(volumenSource);
            VolumeRecord _volumeRecord = JsonConvert.DeserializeObject<VolumeRecord>(volumeJson);
              
            if (string.IsNullOrEmpty(volumeJson))
                throw new Exception("Volume empty");

            Dictionary<string, List<string>> tablePropertiesNames = Get(table);
            List<string> propertiesList = new();
            foreach (var property in tablePropertiesNames.Values.First())
            {
                propertiesList.Add(property);
            }
                        
            VolumeBase _volumeBase = new();          
            _volumeBase.Schema = propertiesList;
            _volumeBase.Rows = new();
            _volumeBase.Name = tablePropertiesNames.Keys.First();
            
            VolumeData _volumeData = new();
            _volumeData.Table = _volumeBase;
            _volumeRecord.VolumeData = (_volumeRecord.VolumeData is null)? new() : _volumeRecord.VolumeData;
            _volumeRecord.VolumeData.Add(_volumeData);

            using (var fs = new FileStream(volumenSource, FileMode.Create))
            using (var sw = new StreamWriter(fs))
            {
                sw.WriteLine(JsonConvert.SerializeObject(_volumeRecord));
            }
 
            return true;
        }
        catch (Exception ex)
        { 
            Console.WriteLine(ex.ToString());
        }
         
        return false;
    }
     
    public static Dictionary<string,List<string>> Get<T>(T tables) where T : new()
    {
        List<string> output = new();
        Dictionary<string, List<string>> outputs = new();
        try
        {
            T tableInstance = new();
            Type tableInstanceType = tableInstance.GetType();
          
            PropertyInfo[] tablePropertyInfos = tableInstanceType.GetProperties();
            if (tablePropertyInfos.Length > 0)
            {
                foreach (var tablePropertyInfo in tablePropertyInfos)
                {
                    output.Add(tablePropertyInfo.Name);
                }
            }

            FieldInfo[] tableFieldInfos = tableInstanceType.GetFields();
            if (tableFieldInfos.Length > 0)
            {
                foreach (var tableFieldInfo in tableFieldInfos)
                {
                    output.Add(tableFieldInfo.Name);
                }
            }

          
            outputs.Add(tableInstance.ToString(), output);

           
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return outputs;

    }
     
}
