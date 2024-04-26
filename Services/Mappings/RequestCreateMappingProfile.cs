using AutoMapper;
using api_scango.Domain.Dtos;
using api_scango.Domain.Entities;

namespace api_scango.Services.Mappings;

public class RequestMappingProfile : Profile
{
    public RequestMappingProfile(){
        CreateMap<ClienteCreateDTO, Cliente>();
        CreateMap<ProductoCreateDto, Producto>();
         CreateMap<ProductosEnCarrito, ProductoEnCarritoDto>().ReverseMap();
         CreateMap<EstablecimientoDTO, Establecimiento>();
         CreateMap<ClienteLoginDTO, Cliente>();
         CreateMap<EstablecimientoCreateDTO, Establecimiento>();
         CreateMap<Establecimiento, EstablecimientoCreateDTO>();


         CreateMap<Producto, ProductoCreateDto>();
         CreateMap<ProductoCreateDto, Producto>();

         CreateMap<ProductoUpdateDTO, Producto>();
         


        //  Administradores
         CreateMap<Administrador, AdministradorCreateDTO>();
         CreateMap<AdministradorCreateDTO, Administrador>();







    }
    
}