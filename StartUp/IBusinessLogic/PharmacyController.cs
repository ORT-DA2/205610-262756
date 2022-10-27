using Microsoft.AspNetCore.Mvc;
using StartUp.IBusinessLogic;
using StartUp.Models.Models.In;
using StartUp.Models.Models.Out;
using StartUp.WebApi.Filters;
using System.Linq;

namespace StartUp.WebApi.Controllers
{
    [Route("api/pharmacy")]
    [ApiController]
    [AuthorizationFilter("administrator")]
    public class PharmacyController : ControllerBase
    {
        private readonly IPharmacyService _pharmacyService;

        public PharmacyController(IPharmacyService service)
        {
            _pharmacyService = service;
        }

        [HttpGet]
        public IActionResult GetPharmacy([FromQuery] PharmacySearchCriteriaModel searchCriteria)
        {
            var retrievedPharmacy = _pharmacyService.GetAllPharmacy(searchCriteria.ToEntity());
            return Ok(retrievedPharmacy.Select(p => new PharmacyBasicModel(p)));
        }


        [HttpGet("{pharmacyId}", Name = "GetPharmacy")]
        public IActionResult GetPharmacy(int pharmacyId)
        {
            var retrievedPharmacy = _pharmacyService.GetSpecificPharmacy(pharmacyId);
            return Ok(new PharmacyDetailModel(retrievedPharmacy));
        }

        [HttpPost]
        public IActionResult CreatePharmacy([FromBody] PharmacyModel newPharmacy)
        {
            var createdPharmacy = _pharmacyService.CreatePharmacy(newPharmacy.ToEntity());
            var pharmacyModel = new PharmacyDetailModel(createdPharmacy);
            return CreatedAtRoute("GetPharmacy", new { pharmacyId = pharmacyModel.Id }, pharmacyModel);
        }

        [HttpPut("{pharmacyId}")]
        public IActionResult UpdatePharmacy(int pharmacyId, [FromBody] PharmacyModel updatedPharmacy)
        {
            var retrievedPharmacy = _pharmacyService.UpdatePharmacy(pharmacyId, updatedPharmacy.ToEntity());
            return Ok(new PharmacyDetailModel(retrievedPharmacy));
        }

        [HttpDelete("{pharmacyId}")]
        public IActionResult DeletePharmacy(int pharmacyId)
        {
            _pharmacyService.DeletePharmacy(pharmacyId);
            return Ok();
        }
    }
}
