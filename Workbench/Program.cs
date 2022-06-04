using JsonFileDB.Service;
using JsonFileDB.Volumes; 
 
JDB TestJDB = new JDB(
    new JDBVolumes()
    {
        Name = "TestVolume_2.json"
    });
 
Console.WriteLine(TestJDB.AddTable(new TableTest()));
Console.WriteLine(TestJDB.AddTable(new TableTest2()));
 
Console.ReadLine();
 
public class TableTest
{
    public Guid Id { get; set; } = default(Guid);
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
}

public class TableTest2
{
    public Guid Id { get; set; } = default(Guid);
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
}

