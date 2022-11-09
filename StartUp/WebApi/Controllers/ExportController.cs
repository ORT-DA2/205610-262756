using Microsoft.AspNetCore.Mvc;
using StartUp.IBusinessLogic;
using StartUp.ModelsExporter;
using StartUp.WebApi.Filters;
using System.Collections.Generic;

namespace StartUp.WebApi.Controllers
{

    [Route("api/exporter")]
    [ApiController]
    [AuthorizationFilter("employee")]
    public class ExportController : ControllerBase
    {
        private readonly IExporterService _exporterService;

        public ExportController(IExporterService exporterService)
        {
            _exporterService = exporterService;
        }

        [HttpGet]
        public IActionResult GetExporters()
        {
            List<string> retrievedExporters = _exporterService.GetAllExporters();
            return Ok(retrievedExporters);
        }

        [HttpPost]
        public IActionResult ExportMedicines([FromBody] ModelExporter model)
        {
            _exporterService.ExportMedicines(model.RouteName, model.Format);
            return Ok($"All the drugs in your pharmacy have been exported in the format {model.Format} in the path {model.RouteName}.{model.Format}");
        }
    }
}
