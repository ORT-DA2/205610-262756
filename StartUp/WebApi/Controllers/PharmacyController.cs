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
        private readonly IPharmacyService _pharmacyService;

        public PharmacyController(IPharmacyService service)
        {
            _pharmacyService = service;
        }

        // Index - Get all pharmacy (/api/pharmacy)
        [HttpGet]
        public IActionResult GetPharmacy([FromQuery] PharmacySearchCriteriaModel searchCriteria)
        {
            var retrievedPharmacy = _pharmacyService.GetAllPharmacy(searchCriteria.ToEntity());
            return Ok(retrievedPharmacy.Select(p => new PharmacyBasicModel(p)));
        }

        // Show - Get specific pharmacy (/api/pharmacy/{id})
        [HttpGet("{pharmacyId}", Name = "GetPharmacy")]
        public IActionResult GetPharmacy(int pharmacyId)
        {
            try
            {
                var retrievedPharmacy = _pharmacyService.GetSpecificPharmacy(pharmacyId);
                return Ok(new PharmacyDetailModel(retrievedPharmacy));
            }
            catch (ResourceNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        // Create - Create new pharmacy (/api/pharmacy)
        [HttpPost]
        public IActionResult CreatePharmacy([FromBody] PharmacyModel newPharmacy)
        {
            try
            {
                var createdPharmacy = _pharmacyService.CreatePharmacy(newPharmacy.ToEntity());
                var pharmacyModel = new PharmacyDetailModel(createdPharmacy);
                return CreatedAtRoute("GetPharmacy", new { pharmacyId = pharmacyModel.Id }, pharmacyModel);
            }
            catch (InvalidResourceException e)
            {
                return BadRequest(e.Message);
            }
        }

        // Update - Update specific pharmacy (/api/pharmacy/{id})
        [HttpPut("{pharmacyId}")]
        public IActionResult UpdatePharmacy(int pharmacyId, [FromBody] PharmacyModel updatedPharmacy)
        {
            try
            {
                var retrievedPharmacy = _pharmacyService.UpdatePharmacy(pharmacyId, updatedPharmacy.ToEntity());
                return Ok(new PharmacyDetailModel(retrievedPharmacy));
            }
            catch (InvalidResourceException e)
            {
                return BadRequest(e.Message);
            }
            catch (ResourceNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        // Delete - Delete specific pharmacy (/api/pharmacy/{id})
        [HttpDelete("{pharmacyId}")]
        public IActionResult DeletePharmacy(int pharmacyId)
        {
            try
            {
                _pharmacyService.DeletePharmacy(pharmacyId);
                return Ok();
            }
            catch (ResourceNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

    }
}
