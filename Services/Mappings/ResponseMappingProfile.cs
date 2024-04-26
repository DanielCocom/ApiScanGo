using AutoMapper;
using api_scango.Domain.Entities;
using api_scango.Domain.Dtos;
using api_scango.Domain.Entities.Dtos;



namespace api_scango.Services.Mappings;

public class ReponseMappingProfile : Profile
{
    public ReponseMappingProfile() 
    { 
        CreateMap<Cliente, ClienteDto>()
        .ForMember( dest => dest.NumeroTelefonico, opt => opt.MapFrom(src => src.NumeroTelefono))
        .ForMember( dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
        .ForMember( dest => dest.Apellido, opt => opt.MapFrom(src => src.Apellido))
        .ForMember( dest => dest.Correo, opt => opt.MapFrom(src => src.Correo));
        // .ForMember( dest => dest.Contraseña, opt => opt.MapFrom(src => src.Contraseña));
        
        CreateMap<Producto, ProductoDto>();

        CreateMap<Producto, ProductoCreateDto>()
        .ForMember( dest => dest.IdProducto, opt => opt.MapFrom(src => src.IdProducto))
        .ForMember( dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
        .ForMember( dest => dest.Precio, opt => opt.MapFrom(src => src.Precio))
        // .ForMember( dest => dest.Imagen, opt => opt.MapFrom(src => src.Imagen))
        .ForMember( dest => dest.IdTipoProducto, opt => opt.MapFrom(src => src.IdTipoProducto))
        .ForMember( dest => dest.IdDescuento, opt => opt.MapFrom(src => src.IdDescuento));



        // // .ForMember( dest => dest.Cantidad, opt => opt.MapFrom(src => src.Cantidad));
        // // .ForMember( dest => dest.Imagen, opt => opt.MapFrom(src => src.Imagen));

         CreateMap<ProductosEnCarrito, ProductoEnCarritoDto>()
                // .ForMember(dest => dest.IdCarrito, opt => opt.MapFrom(src => src.IdCarrito))
                .ForMember(dest => dest.Codigodebarras, opt => opt.MapFrom(src => src.IdProducto))
                .ForMember(dest => dest.Cantidad, opt => opt.MapFrom(src => src.Cantidad))
                .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Total))
                 .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.NombreProducto ?? "no hay nombre xd"))
                 .ForMember(dest => dest.Precio, opt => opt.MapFrom(src => src.IdProductoNavigation!.Precio));


         CreateMap<Establecimiento, EstablecimientoDTO>()
         .ForMember(dest => dest.idSuper, opt => opt.MapFrom(src => src.IdEstablecimiento))

         .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
         .ForMember(dest => dest.Imagen, opt => opt.MapFrom(src => src.Imagen))
         .ForMember(dest => dest.IdInventar, opt => opt.MapFrom(src => src.IdInventario))
         .ForMember(dest => dest.Direccion, opt => opt.MapFrom(src => src.Direccion))
         .ForMember(dest => dest.Longitud, opt => opt.MapFrom(src => src.Longitud))
         .ForMember(dest => dest.Latitud, opt => opt.MapFrom(src => src.Longitud));

            CreateMap<Administrador, Administrador>()
         .ForMember(dest => dest.IdAdministrador, opt => opt.MapFrom(src => src.IdAdministrador))

         .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
         .ForMember(dest => dest.Correo, opt => opt.MapFrom(src => src.Correo));
        //  .ForMember(dest => dest.IdInventar, opt => opt.MapFrom(src => src.IdInventario))
        //  .ForMember(dest => dest.Direccion, opt => opt.MapFrom(src => src.Direccion))


         CreateMap<Venta, VentasTiendaDTO>()
        .ForMember( dest => dest.IdVenta, opt => opt.MapFrom(src => src.IdVenta))
        .ForMember( dest => dest.FechaVenta, opt => opt.MapFrom(src => src.FechaVenta))
        .ForMember( dest => dest.TotalPagado, opt => opt.MapFrom(src => src.TotalPagado))
        .ForMember( dest => dest.NombreTienda, opt => opt.MapFrom(src => src.IdEstablecimientoNavigation!.Nombre))
        .ForMember( dest => dest.IdTransaccion, opt => opt.MapFrom(src => src.IdTransaccion ?? "Don't transaction exist"));
        


              CreateMap<DetalleVenta, DetalleVentaDTO>()
        .ForMember( dest => dest.IdProducto, opt => opt.MapFrom(src => src.IdProducto))
        .ForMember( dest => dest.NombreProducto, opt => opt.MapFrom(src => src.IdProductoNavigation!.Nombre ?? "No cuenta con nombre"))
        .ForMember( dest => dest.Cantidad, opt => opt.MapFrom(src => src.Cantidad))
        .ForMember( dest => dest.PrecioUnitario, opt => opt.MapFrom(src => src.PrecioUnitario))
        .ForMember( dest => dest.Total, opt => opt.MapFrom(src => src.Total));


        CreateMap<Producto, InventarioDTO>()
        .ForMember( dest => dest.IdProducto, opt => opt.MapFrom(src => src.IdProducto))
        .ForMember( dest => dest.NombreProducto, opt => opt.MapFrom(src => src.Nombre))
        .ForMember( dest => dest.IdProducto, opt => opt.MapFrom(src => src.IdProducto))
        .ForMember( dest => dest.Imagen, opt => opt.MapFrom(src => src.Imagen))
        .ForMember( dest => dest.Precio, opt => opt.MapFrom(src => src.Precio))
        .ForMember( dest => dest.Cantidad, opt => opt.MapFrom(src => 20))
        .ReverseMap();


        CreateMap<Compra, CompraDTO>()
        .ForMember(dest => dest.Establecimiento, opt => opt.MapFrom(src => src.IdEstablecimientoNavigation!.Nombre))
        .ForMember(dest => dest.TotalProductos, OPT => OPT.MapFrom(src => src.TotalProductos))
        .ForMember(dest => dest.TotalPagado, OPT => OPT.MapFrom(src => src.TotalPagado))
        .ForMember(dest => dest.TotalProductos, OPT => OPT.MapFrom(src => src.TotalProductos))
        .ForMember(dest => dest.FechaCompra, OPT => OPT.MapFrom(src => src.FechaCompra))
        .ReverseMap();

        
        
        


        // CreateMap<Venta, VentaMesDTO>()
        // .ForMember( dest => dest.Anio, opt => opt.MapFrom(src => src.FechaVenta!.Value.Year))
        // .ForMember( dest => dest.Mes, opt => opt.MapFrom(src => src.FechaVenta.Value.Month))
        // .ForMember( dest => dest.TotalPagado, opt => opt.MapFrom(src => src.TotalPagado))

    }

}