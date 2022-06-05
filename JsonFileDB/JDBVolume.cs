using Newtonsoft.Json;
using System.Text;
using JsonFileDB.Modules.Volumes;

namespace JsonFileDB.Volumes;

/// <summary>
/// Volumes class
/// </summary>
public class JDBVolume
{    
    /// <summary>
    /// Volume path
    /// </summary>
    public string? PathJson { get; set; } = default!;

    /// <summary>
    /// Volume name
    /// </summary>
    public string Name { get; set; } = default!;

    /// <summary>
    /// Volume full path source
    /// </summary>
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

    /// <summary>
    /// Save volume to file
    /// </summary>
    /// <param name="recordToParse"></param>
    internal void Save(VolumeRecord recordToParse)
    { 
        using (var fs = new FileStream(VolumeSource, FileMode.Create))
        using (var sw = new StreamWriter(fs))
        {
            sw.WriteLine(JsonConvert.SerializeObject(recordToParse));
        }
    }

    /// <summary>
    /// Load volume json file
    /// </summary>
    /// <returns></returns>
    internal string Load()
    {
        string fileContents;
        using (StreamReader reader = new StreamReader(VolumeSource))
        {
            fileContents = reader.ReadToEnd();
        }
        return fileContents;
    }

    /// <summary>
    /// Check if volume exist
    /// </summary>
    /// <param name="file">Volume file</param>
    /// <returns>bool</returns>
    private static bool IfVolumeSourceExist(string file) 
        => File.Exists(file);
     
}
