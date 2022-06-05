// TODO: Añadir context layer
// TODO: Añadir obtener Row por key
// TODO: Añadir opción editar Row
// TODO: Añadir opción eliminar Row
// TODO: Escapar comillas en contenidos 
// TODO: Quitar dependencía a Newtonsoft.Json

// Tables models
User userEntity = new();

// Define new Volume
JFDB VolumeTest = new(
    new JDBVolume("TestVolumen.json"),
    new JDBTables()
);

// Create volume
VolumeTest.CreateVolume();

// Define new tables
List<object> tables = new() { 
    new User(),
    new Roles(),
    new Posts(),
}; 
 
// Create tables
foreach (var table in tables)
{
    VolumeTest.AddTable(table);
}
   
// Add registres to table
Guid idRole = VolumeTest.Insert(new Roles()
{
    Name = "Admin"
});
  
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

