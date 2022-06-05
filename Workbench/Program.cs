// TODO: Añadir Global using
// TODO: Añadir obtener Row por key
// TODO: Añadir opción editar Row
// TODO: Añadir opción eliminar Row
// TODO: Escapar comillas en contenidos 
// TODO: Quitar dependencía a Newtonsoft.Json
 
// Define Volume config
string VolumesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AppDomain.CurrentDomain.RelativeSearchPath ?? "");
string VolumeName = "TestVolumen2.json";
string VolumeSource = $"{VolumesPath}{VolumeName}";

// New Volume
JFDB VolumeTest = new JFDB(
    new JDBVolume()
    {
        Name = VolumeName,
        PathJson = VolumesPath,
        VolumeSource = VolumeSource
    },
    new JDBTables());

// Create volume
VolumeTest.CreateVolume();

// Tables models
User userEntity = new();
Roles roles = new();

// Add table roles to volume 
VolumeTest.AddTable(roles);

// Add registres to table
Guid idRole = VolumeTest.Insert(new Roles()
{
    Name = "Admin"
});

// Add table user to volume 
VolumeTest.AddTable(userEntity);
 
// Add registres to table
VolumeTest.Insert(new User()
{
    Name = "David",
    Email = "Pepe@test.com",
    IdRol = idRole,
});
 
VolumeTest.Insert(new User()
{
    Name = "Pepe",
    Email = "Pepe@test.com",
    IdRol = idRole,
});
  
foreach(User users in VolumeTest.GetAll(userEntity))
{
    Console.WriteLine($"Id: {users.Id}");
    Console.WriteLine($"IdRol: {users.IdRol}");
    Console.WriteLine($"Name: {users.Name}");
    Console.WriteLine($"Email: {users.Email}\r\n");
}

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

