using JsonFileDB.Service;
using JsonFileDB.Volumes;

string VolumesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AppDomain.CurrentDomain.RelativeSearchPath ?? "");
string VolumeName = "TestVolumen.json";
string VolumeSource = $"{VolumesPath}{VolumeName}";

JDB TestJDB = new JDB(
    new JDBVolumes()
    {
        Name = VolumeName,
        PathJson = VolumesPath,
        VolumeSource = VolumeSource
    });

//TestJDB.Create();

//TestJDB.AddTable(new User());
//TestJDB.AddTable(new Roles());

Console.WriteLine(
    TestJDB.Insert(new User() {
        Email = "test@test.com"
    })
);
Console.WriteLine(
   TestJDB.Insert(new User()
   {
       Email = "testw@test.com"
   })
);
 
Console.WriteLine(
    TestJDB.Insert(new Roles()
    {
        Name = "test2@test.com"
    })
);
Console.ReadLine();
 
public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public int IdRol { get; set; } = default!;
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

