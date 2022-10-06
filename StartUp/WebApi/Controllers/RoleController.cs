using Microsoft.AspNetCore.Mvc;
using StartUp.IBusinessLogic;
using StartUp.Models.Models.In;
using StartUp.Models.Models.Out;
using StartUp.WebApi.Filters;
using System.Linq;

namespace StartUp.WebApi.Controllers
{
    [Route("api/role")]
    [ApiController]
    //[AuthorizationFilter("administrator")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService service)
        {
            _roleService = service;
        }

        [HttpGet]
        public IActionResult GetRole([FromQuery] RoleSearchCriteriaModel searchCriteria)
        {
            var retrievedRole = _roleService.GetAllRole(searchCriteria.ToEntity());
            return Ok(retrievedRole.Select(r => new RoleBasicModel(r)));
        }

        [HttpGet("{id}", Name = "GetRole")]
        public IActionResult GetRole(int id)
        {
            var retrievedRole = _roleService.GetSpecificRole(id);
            return Ok(new RoleBasicModel(retrievedRole));
        }

        [HttpPost]
        public IActionResult CreateRole([FromBody] RoleModel newRole)
        {
            var createdRole = _roleService.CreateRole(newRole.ToEntity());
            var RoleModel = new RoleBasicModel(createdRole);
            return CreatedAtRoute("GetRole", new { id = RoleModel.Id }, RoleModel);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] RoleModel updatedRole)
        {
            var retrievedRole = _roleService.UpdateRole(id, updatedRole.ToEntity());
            return Ok(new RoleBasicModel(retrievedRole));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _roleService.DeleteRole(id);
            return Ok();
        }
    }
}
