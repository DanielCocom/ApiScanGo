namespace api_scango.Domain.Dtos;

public class ClienteDto{
    public string? NumeroTelefonico {get; set;}
    public string Nombre { get; set; } = null!;
    public string Apellido { get; set; } = null!;

    public string Correo { get; set; } = null!;
    // public string Contraseña { get; set; } = null!;


}