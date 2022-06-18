public class TableUser 
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid IdRol { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
}

