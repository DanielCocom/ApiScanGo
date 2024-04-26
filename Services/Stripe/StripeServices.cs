

using api_scango.Domain.Entities;
using api_scango.Services.Fetures.compra;
using Microsoft.EntityFrameworkCore;
using Stripe;
using Stripe.BillingPortal;
using Stripe.Checkout;

namespace api_scango.Services.stripe;


public class StripeServices
{

    private readonly IConfiguration _configuration;
    private readonly ScanGoDbContext _context;

    private readonly CompraService  _compraService;


    public StripeServices(IConfiguration configuration, ScanGoDbContext scanGoDbContext, CompraService compraService)
    {
        _configuration = configuration;
        _context = scanGoDbContext;
        _compraService = compraService;

        StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];
    }
    public async Task<string> getCheckout(string phoneNumber, int IdEstablecimiento)
    {
        try
        {
            var cliente = await _context.Cliente
                .Include(c => c.IdCarritoNavigation)
                    .ThenInclude(c => c.ProductosEnCarrito)
                        .ThenInclude(c => c.IdProductoNavigation)
                .FirstOrDefaultAsync(cliente => cliente.NumeroTelefono == phoneNumber);

            if (cliente != null && cliente.IdCarritoNavigation != null && cliente.IdCarritoNavigation.ProductosEnCarrito != null)
            {
                // no se que tan bueno es la asignacion de esta lista
                var listaProductos = new List<ProductosEnCarrito>();

                foreach (var item in cliente.IdCarritoNavigation.ProductosEnCarrito)
                {
                    listaProductos.Add(item);
                    
                }

                    

                if (listaProductos.Count > 0)
                {
                    // devulve la url y registra el id de la transaccion
                    var checkout = await CheckoutProducto(listaProductos);
                    return checkout.Url;
                }
                else
                {
                    throw new Exception("El carrito del cliente está vacío.");
                }
            }
            else
            {
                throw new Exception("No se encontró ningún cliente con el número de teléfono proporcionado o el carrito del cliente está vacío.");
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Error al buscar productos: " + ex.Message);
        }
    }

    private async Task<Stripe.Checkout.Session> CheckoutProducto(List<ProductosEnCarrito> productos)
    {

        var options = new Stripe.Checkout.SessionCreateOptions
        { // asi para que no haya problemas de ambiguedad
            PaymentMethodTypes = new List<string>
            {
                "card"
            },
            LineItems = new List<SessionLineItemOptions>(),
            Mode = "payment",
            SuccessUrl = "https://tudominio.com/pago-exitoso",
            CancelUrl = "https://tudominio.com/pago-cancelado"
        };



        foreach (var product in productos)
        {
            var iteam = new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    Currency = "MXN",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = product.NombreProducto

                    },
                    UnitAmount = (long)(product.IdProductoNavigation!.Precio * 100)!



                },
                Quantity = product.Cantidad
            };
            options.LineItems.Add(iteam);

        }
        var service = new Stripe.Checkout.SessionService();
        var session = await service.CreateAsync(options);

        
        return session;
    }
}
