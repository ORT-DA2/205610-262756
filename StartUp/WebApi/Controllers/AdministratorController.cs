using System.Linq;
using IBusinessLogic;
using Microsoft.AspNetCore.Mvc;
using StartUp.Models.Models.In;
using StartUp.Models.Models.Out;
using StartUp.WebApi.Filters;

namespace StartUp.WebApi.Controllers
{

    [Route("api/administrator")]
    [ApiController]
    [RolFilter("administrator")]
    public class AdministratorController : ControllerBase
    {
        private readonly IAdministratorManager _adminManager;

        public AdministratorController(IAdministratorManager manager)
        {
            _adminManager = manager;
        }

        // Index - Get all administrator (/api/administrator)
        [HttpGet]
        public IActionResult GetAdministrator([FromQuery] AdministratorSearchCriteriaModel searchCriteria)
        {
            var retrievedAdmins = _adminManager.GetAllAdministrator(searchCriteria.ToEntity());
            return Ok(retrievedAdmins.Select(a => new AdministratorBasicModel(a)));
        }

        // Show - Get specific administrator (/api/administrator/{id})
        [HttpGet("{id}", Name = "GetAdmin")]
        public IActionResult GetAdministrator(int id)
        {
            var retrievedAdmin = _adminManager.GetSpecificAdministrator(id);
            return Ok(new AdministratorDetailModel(retrievedAdmin));
        }

        // Create - Create new administrator (/api/administrator)
        [HttpPost]
        public IActionResult CreateAdministrator([FromBody] AdministratorModel newAdministrator)
        {
            var createdAdministrator = _adminManager.CreateAdministrator(newAdministrator.ToEntity());
            var adminModel = new AdministratorDetailModel(createdAdministrator);
            return CreatedAtRoute("GetAdmin", new { id = adminModel.Id }, adminModel);
        }

        // Update - Update specific administrator (/api/administrator/{id})
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] AdministratorModel updatedAdmin)
        {
            var retrievedAdmin = _adminManager.UpdateAdministrator(id, updatedAdmin.ToEntity());
            return Ok(new AdministratorDetailModel(retrievedAdmin));
        }

        // Delete - Delete specific administrator (/api/administrator/{id})
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _adminManager.DeleteAdministrator(id);
            return Ok();
        }
    }
}
