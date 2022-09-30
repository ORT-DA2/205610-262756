using System.Linq;
using IBusinessLogic;
using Microsoft.AspNetCore.Mvc;
using StartUp.Models.Models.In;
using StartUp.Models.Models.Out;
using StartUp.WebApi.Filters;

namespace StartUp.WebApi.Controllers
{

    [Route("api/user")]
    [ApiController]
    //[RolFilter("administrator")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService service)
        {
            _userService = service;
        }

        // Index - Get all user (/api/user)
        [HttpGet]
        public IActionResult GetUserByRol([FromQuery] UserSearchCriteriaModel searchCriteria)
        {
            var retrievedUser = _userService.GetAllUser(searchCriteria.ToEntity());
            return Ok(retrievedUser.Select(a => new UserBasicModel(a)));
        }

        // Show - Get specific user (/api/user/{id})
        [HttpGet("{id}", Name = "GetUser")]
        public IActionResult GetUser(int id)
        {
            var retrievedUser = _userService.GetSpecificUser(id);
            return Ok(new UserDetailModel(retrievedUser));
        }

        // Create - Create new user (/api/user)
        [HttpPost]
        public IActionResult CreateUser([FromBody] UserModel newUser)
        {
            var createdUser = _userService.CreateUser(newUser.ToEntity());
            var userModel = new UserDetailModel(createdUser);
            return CreatedAtRoute("GetUser", new { id = userModel.Id }, userModel);
        }

        // Update - Update specific user (/api/user/{id})
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UserModel updatedUser)
        {
            var retrievedUser = _userService.UpdateUser(id, updatedUser.ToEntity());
            return Ok(new UserDetailModel(retrievedUser));
        }

        // Delete - Delete specific user (/api/user/{id})
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _userService.DeleteUser(id);
            return Ok();
        }
    }
}
