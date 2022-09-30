using Microsoft.AspNetCore.Mvc;
using StartUp.Exceptions;
using StartUp.IBusinessLogic;
using StartUp.Models.Models.In;
using StartUp.Models.Models.Out;
using System.Linq;

namespace StartUp.WebApi.Controllers
{
    [Route("api/owner")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        private readonly IOwnerService _ownerService;

        public OwnerController(IOwnerService manager)
        {
            _ownerService = manager;
        }

        // Index - Get all owner (/api/owner)
        [HttpGet]
        public IActionResult GetOwner([FromQuery] OwnerSearchCriteriaModel searchCriteria)
        {
            var retrievedOwners = _ownerService.GetAllOwner(searchCriteria.ToEntity());
            return Ok(retrievedOwners.Select(o => new OwnerBasicModel(o)));
        }

        // Show - Get specific owner (/api/owner/{id})
        [HttpGet("{id}")]
        public IActionResult GetOwner(int id)
        {
            var retrievedOwner = _ownerService.GetSpecificOwner(id);
            return Ok(new OwnerDetailModel(retrievedOwner));
        }

        // Create - Create new owner (/api/owner)
        [HttpPost]
        public IActionResult CreateOwner([FromBody] OwnerModel newOwner)
        {
            var createdOwner = _ownerService.CreateOwner(newOwner.ToEntity());
            var ownerModel = new OwnerDetailModel(createdOwner);
            return CreatedAtRoute("GetOwner", new { id = ownerModel.Id }, ownerModel);
        }

        // Update - Update specific owner (/api/owner/{id})
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] OwnerModel updatedOwner)
        {
            var retrievedOwner = _ownerService.UpdateOwner(id, updatedOwner.ToEntity());
            return Ok(new OwnerDetailModel(retrievedOwner));
        }

        // Delete - Delete specific owner (/api/owner/{id})
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _ownerService.DeleteOwner(id);
            return Ok();
        }
    }
}
