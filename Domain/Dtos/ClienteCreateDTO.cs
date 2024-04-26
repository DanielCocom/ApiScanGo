namespace api_scango.Domain.Dtos;
public  class ClienteCreateDTO
{
    public string? NumeroTelefono { get; set; }

    public string Nombre { get; set; } = null!;
    public string Apellido { get; set; } = null!;


    public string Correo { get; set; } = null!;

    public string Contrasena { get; set; } = null!;
}