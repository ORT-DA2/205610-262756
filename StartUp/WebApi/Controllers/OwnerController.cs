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
            private readonly IOwnerManager _ownerManager;

            public OwnerController(IOwnerManager manager)
            {
                _ownerManager = manager;
            }

            // Index - Get all owner (/api/owner)
            [HttpGet]
            public IActionResult GetOwner([FromQuery] OwnerSearchCriteriaModel searchCriteria)
            {
                var retrievedOwners = _ownerManager.GetAllOwner(searchCriteria.ToEntity());
                return Ok(retrievedOwners.Select(o => new OwnerBasicModel(o)));
            }

            // Show - Get specific owner (/api/owner/{id})
            [HttpGet]
            public IActionResult GetOwner(int id)
            {
                try
                {
                    var retrievedOwner = _ownerManager.GetSpecificOwner(id);
                    return Ok(new OwnerDetailModel(retrievedOwner));
                }
                catch (ResourceNotFoundException e)
                {
                    return NotFound(e.Message);
                }
            }

            // Create - Create new owner (/api/owner)
            [HttpPost]
            public IActionResult CreateOwner([FromBody] OwnerModel newOwner)
            {
                try
                {
                    var createdOwner = _ownerManager.CreateOwner(newOwner.ToEntity());
                    var ownerModel = new OwnerDetailModel(createdOwner);
                    return CreatedAtRoute("GetOwner", new { id = ownerModel.Id }, ownerModel);
                }
                catch (InvalidResourceException e)
                {
                    return BadRequest(e.Message);
                }
            }

            // Update - Update specific owner (/api/owner/{id})
            [HttpPut("{id}")]
            public IActionResult Update(int id, [FromBody] OwnerModel updatedOwner)
            {
                try
                {
                    var retrievedOwner = _ownerManager.UpdateOwner(id, updatedOwner.ToEntity());
                    return Ok(new OwnerDetailModel(retrievedOwner));
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

            // Delete - Delete specific owner (/api/owner/{id})
            [HttpDelete("{id}")]
            public IActionResult Delete(int id)
            {
                try
                {
                    _ownerManager.DeleteOwner(id);
                    return Ok();
                }
                catch (ResourceNotFoundException e)
                {
                    return NotFound(e.Message);
                }
            }
        }
    }
