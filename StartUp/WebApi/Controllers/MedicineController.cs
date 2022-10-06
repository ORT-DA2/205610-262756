using Microsoft.AspNetCore.Mvc;
using StartUp.Exceptions;
using StartUp.IBusinessLogic;
using StartUp.Models.Models.In;
using StartUp.Models.Models.Out;
using StartUp.WebApi.Filters;
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

        [HttpGet]
        public IActionResult GetMedicine([FromQuery] MedicineSearchCriteriaModel searchCriteria)
        {
            var retrievedMedicines = _medicineService.GetAllMedicine(searchCriteria.ToEntity());
            return Ok(retrievedMedicines.Select(m => new MedicineBasicModel(m)));
        }

        [HttpGet("{id}", Name = "GetMedicine")]
        public IActionResult GetMedicine(int id)
        {
            var retrievedMedicine = _medicineService.GetSpecificMedicine(id);
            return Ok(new MedicineDetailModel(retrievedMedicine));
        }

        [HttpPost]
        //[AuthorizationFilter("employee")]
        public IActionResult CreateMedicine([FromBody] MedicineModel newMedicine)
        {
            var createdMedicine = _medicineService.CreateMedicine(newMedicine.ToEntity());
            var medicineModel = new MedicineDetailModel(createdMedicine);
            return CreatedAtRoute("GetMedicine", new { id = medicineModel.Id }, medicineModel);
        }

        [HttpPut("{id}")]
        //[AuthorizationFilter("employee")]
        public IActionResult Update(int id, [FromBody] MedicineModel updatedMedicine)
        {
            var retrievedMedicine = _medicineService.UpdateMedicine(id, updatedMedicine.ToEntity());
            return Ok(new MedicineDetailModel(retrievedMedicine));
        }

        [HttpDelete("{id}")]
        //[AuthorizationFilter("employee")]
        public IActionResult Delete(int id)
        {
            _medicineService.DeleteMedicine(id);
            return Ok($"Medicine {id} deleted");
        }
    }
}
