using api_scango.Infrastructure.Data.Repositories;
using api_scango.Domain.Dtos;
using AutoMapper;
using api_scango.Domain.Entities;

public class VentaServices
{
    private readonly VentaRepository _repository;
    public VentaServices(VentaRepository ventaRepository)
    {
        _repository = ventaRepository;
    }
    public async Task<IEnumerable<Venta>> getAll()
    {
        return await _repository.GetAll();
    }
    public async Task<List<ProductosVendidosDTO>> getProductosMasVendidos(int idEstablecimiento)
    {
        return await _repository.getProductMasVendidos(idEstablecimiento);
    }
    public async Task<List<ProductosVendidosDTO>> getProductosMenosVendidos(int idEstablecimiento)
    {
        return await _repository.getProductoMenosVendido(idEstablecimiento);
    }
    public async Task<List<VentasTiendaDTO>> getVentasEstablecimientoId(int idEstablecimiento)
    {
        var nose = await _repository.getVentasEstablecimiento(idEstablecimiento);
        return nose;
    }
    public async Task<List<DetalleVenta>> GetDetalleVentaId(int idVenta)
    {
        return await _repository.GetDetalleVentaId(idVenta);
    }
    public async Task<List<VentasTiendaDTO>> GetLastFiveSale(int idEstablecimiento){
        return await _repository.GetLastFiveSale(idEstablecimiento);
    }
     public async Task<List<VentaMesDTO>> GetSalesPerMonthStore(int idEstablecimiento){
        return await _repository.GetSalesPerMonthStore(idEstablecimiento);
    }
}