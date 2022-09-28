using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using StartUp.Exceptions;
using StartUp.IBusinessLogic;
using StartUp.Models.Models.In;
using StartUp.Models.Models.Out;
using System.Linq;

namespace StartUp.WebApi.Controllers
{
    [Route("api/pharmacy")]
    [ApiController]
    public class PharmacyController : ControllerBase
    {
        private readonly IPharmacyManager _pharmacyManager;

        public PharmacyController(IPharmacyManager manager)
        {
            _pharmacyManager = manager;
        }

        // Index - Get all pharmacy (/api/pharmacy)
        [HttpGet]
        public IActionResult GetPharmacy([FromQuery] PharmacySearchCriteriaModel searchCriteria)
        {
            var retrievedPharmacy = _pharmacyManager.GetAllPharmacy(searchCriteria.ToEntity());
            return Ok(retrievedPharmacy.Select(p => new PharmacyBasicModel(p)));
        }

        // Show - Get specific pharmacy (/api/pharmacy/{id})
        [HttpGet("{pharmacyId}", Name = "GetPharmacy")]
        public IActionResult GetPharmacy(int pharmacyId)
        {
            var retrievedPharmacy = _pharmacyManager.GetSpecificPharmacy(pharmacyId);
            return Ok(new PharmacyDetailModel(retrievedPharmacy));
        }

        // Create - Create new pharmacy (/api/pharmacy)
        [HttpPost]
        [Filters(Authotization)]
        public IActionResult CreatePharmacy([FromBody] PharmacyModel newPharmacy)
        {
            var createdPharmacy = _pharmacyManager.CreatePharmacy(newPharmacy.ToEntity());
            var pharmacyModel = new PharmacyDetailModel(createdPharmacy);
            return CreatedAtRoute("GetPharmacy", new { pharmacyId = pharmacyModel.Id }, pharmacyModel);
        }

        // Update - Update specific pharmacy (/api/pharmacy/{id})
        [HttpPut("{pharmacyId}")]
        public IActionResult UpdatePharmacy(int pharmacyId, [FromBody] PharmacyModel updatedPharmacy)
        {
            var retrievedPharmacy = _pharmacyManager.UpdatePharmacy(pharmacyId, updatedPharmacy.ToEntity());
            return Ok(new PharmacyDetailModel(retrievedPharmacy));
        }

        // Delete - Delete specific pharmacy (/api/pharmacy/{id})
        [HttpDelete("{pharmacyId}")]
        public IActionResult DeletePharmacy(int pharmacyId)
        {
            _pharmacyManager.DeletePharmacy(pharmacyId);
            return Ok();
        }
    }
}
