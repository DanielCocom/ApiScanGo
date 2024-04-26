


using api_scango.Services.Fetures.producto;
using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("V1/[controller]")]
public class ReporteMensualController : ControllerBase
{
    private readonly ReporteServices _service;


    public ReporteMensualController(ReporteServices reporteServices){
        _service = reporteServices;
    }
    [HttpGet]
    public async Task<IActionResult> GetReporteMensual(int idEstablecimiento, DateTime dateTime){
        var reporteMensual = await _service.GetReporteMensual(idEstablecimiento, dateTime);
        return Ok(reporteMensual);
    }
}