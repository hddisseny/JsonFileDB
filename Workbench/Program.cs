// Tables models 
TableUser UserCtx = new();
TableRoles RolesCtx = new();
TablePosts TableCtx = new();

// Define new Volume 
JFDB VolumeTest = new(
    new JDBVolume("TestVolumen.json"),
    new()
    {
        UserCtx,
        RolesCtx,
        TableCtx,
    }
);

// Add role to table
RolesCtx.Name = "Admin"; 
Guid idRole = VolumeTest.Insert(RolesCtx);

// Add user to table
UserCtx.Name = "David";
UserCtx.Email = "david@test.com";
UserCtx.IdRol = idRole;
VolumeTest.Insert(UserCtx);

// Add usesr to table
UserCtx.Name = "Pepe";
UserCtx.Email = "pepe@test.com";
UserCtx.IdRol = idRole;
VolumeTest.Insert(UserCtx);
 
foreach(TableUser users in VolumeTest.GetAll(UserCtx))
{
    Console.WriteLine($"Id: {users.Id}");
    Console.WriteLine($"IdRol: {users.IdRol}");
    Console.WriteLine($"Name: {users.Name}");
    Console.WriteLine($"Email: {users.Email}"); 
}

var outputValue = VolumeTest.Get("Name", "David", new TableUser());

Console.ReadLine();


 
// TODO: Añadir obtener Row por key
// TODO: Añadir opción editar Row
// TODO: Añadir opción eliminar Row
// TODO: Escapar comillas en contenidos 
// TODO: Quitar dependencía a Newtonsoft.Json
