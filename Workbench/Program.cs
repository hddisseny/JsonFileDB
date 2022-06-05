using JsonFileDB.Service;
using JsonFileDB.Volumes;

// Define Volume config
string VolumesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AppDomain.CurrentDomain.RelativeSearchPath ?? "");
string VolumeName = "TestVolumen2.json";
string VolumeSource = $"{VolumesPath}{VolumeName}";

// New Volume
JDB VolumeTest = new JDB(
    new JDBVolumes()
    {
        Name = VolumeName,
        PathJson = VolumesPath,
        VolumeSource = VolumeSource
    });

// Create volume
VolumeTest.CreateVolume();

VolumeTest.AddTable(new Roles());
Guid idRole = VolumeTest.Insert(new Roles()
{
    Name = "Admin"
});

// Add tables to volume
VolumeTest.AddTable(new User());
VolumeTest.Insert(new User()
{
    Name = "David",
    Email = "test@test.com",
    IdRol = idRole
});

 
Console.ReadLine();
 
public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid IdRol { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
}

public class Roles
{
    public Guid Id { get; set; } = Guid.NewGuid(); 
    public string Name { get; set; } = default!;
}

public class Posts
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = default!;
}

