﻿using Microsoft.AspNetCore.Mvc;
using StartUp.IBusinessLogic;
using StartUp.Models.Models.Out;
using System.Collections.Generic;
using System.Linq;

namespace StartUp.WebApi.Controllers
{
    public class ExportController : ControllerBase
    {
        private readonly IExporterService _exporterService;

        public ExportController(IExporterService exporterService)
        {
            _exporterService = exporterService;
        }

        [HttpGet("exporters")]
        public IActionResult GetExporters()
        {
            List<string> retrievedExporters = _exporterService.GetAllExporters();
            return Ok(retrievedExporters);
        }

        [HttpPost("export")]
        public IActionResult ExportMedicines([FromBody] string exporterName)
        {
            List<MedicineBasicModel> exportedMedicine = _exporterService.ExportMedicines(exporterName).Select(pet => new MedicineBasicModel(pet)).ToList();
            return Ok(exportedMedicine);
        }
    }
}
