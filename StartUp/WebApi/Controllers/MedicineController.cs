using Microsoft.AspNetCore.Mvc;
using StartUp.Exceptions;
using StartUp.IBusinessLogic;
using StartUp.Models.Models.In;
using StartUp.Models.Models.Out;
using System.Linq;

namespace StartUp.WebApi.Controllers
{

    [Route("api/medicine")]
    [ApiController]
    public class MedicineController : ControllerBase
    {
        private readonly IMedicineService _medicineService;

        public MedicineController(IMedicineService service)
        {
            _medicineService = service;
        }

        // Index - Get all medicine (/api/medicine)
        [HttpGet]
        public IActionResult GetMedicine([FromQuery] MedicineSearchCriteriaModel searchCriteria)
        {
            var retrievedMedicines = _medicineService.GetAllMedicine(searchCriteria.ToEntity());
            return Ok(retrievedMedicines.Select(m => new MedicineBasicModel(m)));
        }

        // Show - Get specific medicine (/api/medicine/{id})
        [HttpGet("{id}")]
        public IActionResult GetMedicine(int id)
        {
            var retrievedMedicine = _medicineService.GetSpecificMedicine(id);
            return Ok(new MedicineDetailModel(retrievedMedicine));
        }

        // Create - Create new medicine (/api/medicine)
        [HttpPost]
        public IActionResult CreateMedicine([FromBody] MedicineModel newMedicine)
        {
            var createdMedicine = _medicineService.CreateMedicine(newMedicine.ToEntity());
            var medicineModel = new MedicineDetailModel(createdMedicine);
            return CreatedAtRoute("GetMedicine", new { id = medicineModel.Id }, medicineModel);
        }

        // Update - Update specific medicine (/api/medicine/{id})
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] MedicineModel updatedMedicine)
        {
            var retrievedMedicine = _medicineService.UpdateMedicine(id, updatedMedicine.ToEntity());
            return Ok(new MedicineDetailModel(retrievedMedicine));
        }

        // Delete - Delete specific medicine (/api/medicine/{id})
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _medicineService.DeleteMedicine(id);
            return Ok();
        }
    }
}
