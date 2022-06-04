using System.Text;

namespace JsonFileDB.Volumes;
public class VolumeRecord
{
    public string VolumeName { get; set; }
    public List<VolumeData> VolumeData { get; set; }
}

public class VolumeData
{
    public VolumeBase Table { get; set; }

}

public class VolumeBase
{
    public string Name { get; set; }
    public List<string> Schema { get; set; }
    public List<string> Rows { get; set; }
}

public class JDBVolumes
{
    #region Volumenes  
    public string? PathJson { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string VolumeSource { get; set; } = default!;
    internal void Create()
    { 
        try
        {
            PathJson = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AppDomain.CurrentDomain.RelativeSearchPath ?? "");
              
            if (PathJson is null || Name is null) 
                throw new ArgumentNullException(nameof(Name)); 

            if (PathJson.Equals(string.Empty)) 
                throw new ArgumentNullException("Path invalid");

            if (Name.Equals(string.Empty))
                throw new ArgumentNullException("Name invalid");
            
            VolumeSource = $"{PathJson}{Name}";

            var volumeName = Path.ChangeExtension(Name, null); 
            StringBuilder volumeJson = new();
            volumeJson.Append("{");
            volumeJson.Append($"\"VolumeName\":\"{volumeName}\",");
            volumeJson.Append("\"VolumeData\" : \"\"");
            //volumeJson.Append("\"Volume\": {}");
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
    private static bool IfVolumeSourceExist(string file) 
        => File.Exists(file);

    #endregion
}
