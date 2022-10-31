using Microsoft.AspNetCore.Mvc;
using StartUp.IBusinessLogic;
using StartUp.Models.Models.In;
using StartUp.Models.Models.Out;
using StartUp.WebApi.Filters;
using System.Linq;

namespace StartUp.WebApi.Controllers
{

    [Route("api/invitation")]
    [ApiController]
    //[AuthorizationFilter("administrator")]
    public class InvitationController : ControllerBase
    {
        private readonly IInvitationService _invitationManager;

        public InvitationController(IInvitationService manager)
        {
            _invitationManager = manager;
        }

        [HttpGet]
        public IActionResult GetInvitation([FromQuery] InvitationSearchCriteriaModel searchCriteria)
        {
            var retrievedInvitations = _invitationManager.GetAllInvitation(searchCriteria.ToEntity());
            return Ok(retrievedInvitations.Select(i => new InvitationDetailModel(i)));
        }

        [HttpGet("{id}", Name = "GetInvitation")]
        public IActionResult GetInvitation(int id)
        {
            var retrievedInvitation = _invitationManager.GetSpecificInvitation(id);
            return Ok(new InvitationDetailModel(retrievedInvitation));
        }

        [HttpGet("{userName}/{code}", Name = "SpecificInvitation")]
        public IActionResult GetInvitationByUserAndCode(string username, int code)
        {
            var retrievedInvitation = _invitationManager.GetSpecificInvitationByUserAndPass(username, code);
            return Ok(new InvitationDetailModel(retrievedInvitation));
        }

        [HttpPost]
        //[AuthorizationFilter("administrator")]
        public IActionResult CreateInvitation([FromBody] InvitationModel newInvitation)
        {
            var createdInvitation = _invitationManager.CreateInvitation(newInvitation.ToEntity());
            var invitationModel = new InvitationDetailModel(createdInvitation);
            return CreatedAtRoute("GetInvitation", new { id = invitationModel.Id }, invitationModel);
        }

        [HttpPut("{id}")]
        [AuthorizationFilter("administrator")]
        public IActionResult Update(int id, [FromBody] InvitationModel updatedInvitation)
        {
            var retrievedInvitation = _invitationManager.UpdateInvitation(id, updatedInvitation.ToEntity());
            return Ok(new InvitationDetailModel(retrievedInvitation));
        }

        [HttpDelete("{id}")]
        [AuthorizationFilter("administrator")]
        public IActionResult Delete(int id)
        {
            _invitationManager.DeleteInvitation(id);
            return Ok();
        }
    }
}
