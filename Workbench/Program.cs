using JsonFileDB.Service;
using JsonFileDB.Volumes; 
 
JDB TestJDB = new JDB(
    new JDBVolumes()
    {
        Name = "TestVolume_admin.json"
    });
 
Console.WriteLine(TestJDB.AddTable(new User()));
Console.WriteLine(TestJDB.AddTable(new Roles()));
 
Console.ReadLine();
 
public class User
{
    public Guid Id { get; set; } = default(Guid);
    public int IdRol { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
}

public class Roles
{
    public Guid Id { get; set; } = default(Guid); 
    public string Name { get; set; } = default!;
}

