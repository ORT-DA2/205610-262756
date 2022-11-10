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
    //[AuthorizationFilter("administrator")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService service)
        {
            _userService = service;
        }

        [HttpGet]
        public IActionResult GetUserByRol([FromQuery] UserSearchCriteriaModel searchCriteria)
        {
            var retrievedUser = _userService.GetAllUser(searchCriteria.ToEntity());
            return Ok(retrievedUser.Select(a => new UserBasicModel(a)));
        }

        [HttpGet("{username}", Name = "GetUser")]
        public IActionResult GetUser(string username)
        {
            var retrievedUser = _userService.GetSpecificUserByUserName(username);
            return Ok(new UserDetailModel(retrievedUser));
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] UserModel newUser)
        {
            newUser.Pharmacy = newUser.Invitation.Pharmacy;
            var createdUser = _userService.CreateUser(newUser.ToEntity());
            var userModel = new UserDetailModel(createdUser);
            return Ok(userModel);
        }

        [HttpPut("{id}")]
        [AuthorizationFilter("administrator")]
        public IActionResult Update(int id, [FromBody] UserModel updatedUser)
        {
            var retrievedUser = _userService.UpdateUser(id, updatedUser.ToEntity());
            return Ok(new UserDetailModel(retrievedUser));
        }

        [HttpDelete("{id}")]
        [AuthorizationFilter("administrator")]
        public IActionResult Delete(int id)
        {
            _userService.DeleteUser(id);
            return Ok();
        }
    }
}
