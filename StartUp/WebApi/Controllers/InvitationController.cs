using Microsoft.AspNetCore.Mvc;
using StartUp.Exceptions;
using StartUp.IBusinessLogic;
using StartUp.Models.Models.In;
using StartUp.Models.Models.Out;
using System.Linq;

namespace StartUp.WebApi.Controllers
{

        [Route("api/invitation")]
        [ApiController]
        public class InvitationController : ControllerBase
        {
            private readonly IInvitationService _invitationService;

            public InvitationController(IInvitationService service)
            {
                _invitationService = service;
            }

            // Index - Get all invitation (/api/invitation)
            [HttpGet]
            public IActionResult GetInvitation([FromQuery] InvitationSearchCriteriaModel searchCriteria)
            {
                var retrievedInvitations = _invitationService.GetAllInvitation(searchCriteria.ToEntity());
                return Ok(retrievedInvitations.Select(i => new InvitationBasicModel(i)));
            }

            // Show - Get specific invitation (/api/invitation/{id})
            [HttpGet("{id}", Name = "GetInvitation")]
            public IActionResult GetInvitation(int id)
            {
                try
                {
                    var retrievedInvitation = _invitationService.GetSpecificInvitation(id);
                    return Ok(new InvitationDetailModel(retrievedInvitation));
                }
                catch (ResourceNotFoundException e)
                {
                    return NotFound(e.Message);
                }
            }

            // Create - Create new invitation (/api/invitation)
            [HttpPost]
            public IActionResult CreateInvitation([FromBody] InvitationModel newInvitation)
            {
                try
                {
                    var createdInvitation = _invitationService.CreateInvitation(newInvitation.ToEntity());
                    var invitationModel = new InvitationDetailModel(createdInvitation);
                    return CreatedAtRoute("GetInvitation", new { id = invitationModel.Id }, invitationModel);
                }
                catch (InvalidResourceException e)
                {
                    return BadRequest(e.Message);
                }
            }

            // Update - Update specific invitation (/api/invitation/{id})
            [HttpPut("{id}")]
            public IActionResult Update(int id, [FromBody] InvitationModel updatedInvitation)
            {
                try
                {
                    var retrievedInvitation = _invitationService.UpdateInvitation(id, updatedInvitation.ToEntity());
                    return Ok(new InvitationDetailModel(retrievedInvitation));
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

            // Delete - Delete specific invitation (/api/invitation/{id})
            [HttpDelete("{id}")]
            public IActionResult Delete(int id)
            {
                try
                {
                    _invitationService.DeleteInvitation(id);
                    return Ok();
                }
                catch (ResourceNotFoundException e)
                {
                    return NotFound(e.Message);
                }
            }
        }
}
