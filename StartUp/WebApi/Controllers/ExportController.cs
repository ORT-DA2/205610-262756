using Microsoft.AspNetCore.Mvc;
using StartUp.IBusinessLogic;
using System.Collections.Generic;

namespace StartUp.WebApi.Controllers
{

    [Route("api/exporter")]
    [ApiController]
    //[AuthorizationFilter("administrator")]
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
        public IActionResult ExportMedicines([FromBody] string routeName, string format)
        {

            _exporterService.ExportMedicines(routeName, format);
            return Ok();
        }
    }
}
