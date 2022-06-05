using Newtonsoft.Json;
using System.Text;

namespace JsonFileDB.Volumes;

public class JDBVolumes
{
    #region Volumenes  
    public string? PathJson { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string VolumeSource { get; set; } = default!;
    
    /// <summary>
    /// Create the json file of the volume
    /// </summary>
    /// <exception cref="ArgumentNullException"></exception>
    internal void Create()
    { 
        try
        {               
            if (PathJson is null || Name is null) 
                throw new ArgumentNullException(nameof(Name)); 

            if (PathJson.Equals(string.Empty)) 
                throw new ArgumentNullException("Path invalid");

            if (Name.Equals(string.Empty))
                throw new ArgumentNullException("Name invalid");
            
          
            var volumeName = Path.ChangeExtension(Name, null); 
            StringBuilder volumeJson = new();
            volumeJson.Append("{");
            volumeJson.Append($"\"VolumeName\":\"{volumeName}\",");
            volumeJson.Append("\"VolumeData\" : \"\""); 
            volumeJson.Append("}");
             
            using (var fs = new FileStream(VolumeSource, FileMode.Create))
            using (var sw = new StreamWriter(fs))
            {
                sw.WriteLine(volumeJson.ToString());
            }
             
            Console.WriteLine("");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        } 
    }

    internal void Save(VolumeRecord recordToParse)
    { 
        using (var fs = new FileStream(VolumeSource, FileMode.Create))
        using (var sw = new StreamWriter(fs))
        {
            sw.WriteLine(JsonConvert.SerializeObject(recordToParse));
        }
    }

    internal string Load()
    {
        string fileContents;
        using (StreamReader reader = new StreamReader(VolumeSource))
        {
            fileContents = reader.ReadToEnd();
        }
        return fileContents;
    }

    private static bool IfVolumeSourceExist(string file) 
        => File.Exists(file);

    #endregion
}
