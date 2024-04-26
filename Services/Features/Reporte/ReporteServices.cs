using api_scango.Infrastructure.Data.Repositories;
using api_scango.Domain.Entities;
using api_scango.Domain.Dtos;

namespace api_scango.Services.Fetures.producto;

public class ReporteServices{
    private readonly ReporteRepository _repository;

    public ReporteServices(ReporteRepository reporteRepository){
        _repository = reporteRepository;
    }

    public async Task<ReporteMensual> GetReporteMensual(int idEstablecimiento, DateTime dateTime){
        return await _repository.GenerarReporteMes(idEstablecimiento, dateTime);
    } 
}