using api_scango.Domain.Entities;
using MailKit.Net;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;
using api_scango.Domain.Dtos;
using System.Text;
using MailKit.Security;
using Microsoft.EntityFrameworkCore;

public class EmailService
{
    private readonly IConfiguration _configuration;
    private readonly ScanGoDbContext _context;

    public EmailService(IConfiguration configuration, ScanGoDbContext scanGoDbContext)
    {
        _configuration = configuration;
        _context = scanGoDbContext;

    }
    public async Task SendEmail(Cliente cliente, ICollection<ProductosEnCarrito> productosEnCarrito, Venta venta)
    {

        string html = await CreateHtmlEmailContent(productosEnCarrito, venta);
        var request = createDtoEmail(cliente);

        var email = new MimeMessage();
        email.From.Add(new MailboxAddress("FastGo", _configuration.GetSection("Email:User").Value));
        email.To.Add(MailboxAddress.Parse(request.Para));
        email.Subject = request.Contenido;


        var builder = new BodyBuilder();
        builder.HtmlBody = html;
        email.Body = builder.ToMessageBody();

        //Ejecucion del servicio
        using var smtp = new SmtpClient();
        smtp.Connect(
            _configuration.GetSection("Email:Host").Value,
            Convert.ToInt32(_configuration.GetSection("Email:Port").Value),
            SecureSocketOptions.StartTls
        );

        smtp.Authenticate(_configuration.GetSection("Email:User").Value, _configuration.GetSection("Email:Password").Value);

        smtp.Send(email);
        smtp.Disconnect(true);
    }

    private async Task<string> CreateHtmlEmailContent(ICollection<ProductosEnCarrito> productosEnCarrito, Venta venta)
    {
        // no deberia buscar aqui esta madr pero bueno
        var establecimiento = await _context.Establecimiento
         .FirstOrDefaultAsync(e => e.IdEstablecimiento == venta.IdEstablecimiento);

        var body = new StringBuilder();
        body.AppendLine("<div style=\"background-color: #fff; border-radius: 10px; box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1); padding: 20px; max-width: 400px; margin: auto;\">");
        body.AppendLine($"<h1 style=\"text-align: center; color: #800000;\">Gracias pro comprar en: {establecimiento!.Nombre}</h1>");
        body.AppendLine("<div style=\"display: flex; justify-content: space-between; border-bottom: 2px solid black; padding: 15px; margin-bottom: 5px;\">");
        body.AppendLine("<h2 style=\"color: #007bff;\">Resumen de la compra</h2>");
        body.AppendLine($"<h3 style=\"font-weight: bold; margin-left: 30px\">{venta.FechaVenta}</h3>");
        body.AppendLine("</div>");

        foreach (var producto in productosEnCarrito)
        {
            // Contenedor de cada producto
            body.AppendLine("<div style=\"display: flex; margin-bottom: 20px;\">");

            // Detalles del producto (nombre, cantidad, total)
            body.AppendLine("<div style=\"flex: 1;\">");
            body.AppendLine($"<p style=\"font-size: 15px; font-weight: bold;\">{producto.NombreProducto}</p>");
            body.AppendLine($"<p style=\"font-size: 15px; font-weight: bold;\">Cantidad: {producto.Cantidad}</p>");
            body.AppendLine($"<p style=\"font-size: 15px; font-weight: bold;\">Total: {producto.Total}</p>");
            body.AppendLine("</div>");

            // Imagen del producto
            body.AppendLine("<div style=\"margin-left: auto; \">");
            body.AppendLine($"<img src=\"{producto.IdProductoNavigation.Imagen}\" alt=\"{producto.NombreProducto}\" style=\"max-width: 100px; border-radius: 5px;\"/>");
            body.AppendLine("</div>");

            body.AppendLine("</div>"); // Cierre del contenedor de producto
        }

        // Calcular el monto total
        decimal total = (decimal)productosEnCarrito.Sum(p => p.Total);
        body.AppendLine($"<p style=\"font-weight: bold; text-align: center; font-size: 30px\">Monto total: ${total}</p>");
        body.Append("</div>");

        return body.ToString();
    }


    private EmailDTO createDtoEmail(Cliente cliente)
    {
        var emailDto = new EmailDTO
        {
            Para = cliente.Correo,
            Asunto = "Tikect de Compra",
            Contenido = ""
        };

        return emailDto;
    }
}