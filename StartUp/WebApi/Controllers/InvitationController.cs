using Microsoft.AspNetCore.Mvc;
using StartUp.BusinessLogic;
using StartUp.Exceptions;
using StartUp.IBusinessLogic;
using StartUp.Models.Models.In;
using StartUp.Models.Models.Out;
using StartUp.WebApi.Filters;
using System.Linq;

namespace StartUp.WebApi.Controllers
{

    [Route("api/invitation")]
    [ApiController]
    //SOLO TIENEN ACCESO LOS ADMINISTRADORES
    public class InvitationController : ControllerBase
    {
        private readonly IInvitationManager _invitationManager;

        public InvitationController(IInvitationManager manager)
        {
            _invitationManager = manager;
        }

        // Index - Get all invitation (/api/invitation)
        [HttpGet]
        public IActionResult GetInvitation([FromQuery] InvitationSearchCriteriaModel searchCriteria)
        {
            var retrievedInvitations = _invitationManager.GetAllInvitation(searchCriteria.ToEntity());
            return Ok(retrievedInvitations.Select(i => new InvitationBasicModel(i)));
        }

        // Show - Get specific invitation (/api/invitation/{id})
        [HttpGet("{id}", Name = "GetInvitation")]
        public IActionResult GetInvitation(int id)
        {
            var retrievedInvitation = _invitationManager.GetSpecificInvitation(id);
            return Ok(new InvitationDetailModel(retrievedInvitation));
        }

        // Create - Create new invitation (/api/invitation)
        [HttpPost]
        public IActionResult CreateInvitation([FromBody] InvitationModel newInvitation)
        {
            var createdInvitation = _invitationManager.CreateInvitation(newInvitation.ToEntity());
            var invitationModel = new InvitationDetailModel(createdInvitation);
            return CreatedAtRoute("GetInvitation", new { id = invitationModel.Id }, invitationModel);
        }

        // Update - Update specific invitation (/api/invitation/{id})
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] InvitationModel updatedInvitation)
        {
            var retrievedInvitation = _invitationManager.UpdateInvitation(id, updatedInvitation.ToEntity());
            return Ok(new InvitationDetailModel(retrievedInvitation));
        }

        // Delete - Delete specific invitation (/api/invitation/{id})
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _invitationManager.DeleteInvitation(id);
            return Ok();
        }
    }
}
