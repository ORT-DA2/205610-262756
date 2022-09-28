using Microsoft.AspNetCore.Mvc;
using StartUp.Exceptions;
using StartUp.Models.Models.In;
using System;

namespace StartUp.WebApi.Controllers;

[Route("api/sessions")]
[ApiController]
public class SessionController : ControllerBase
{
    // Create - Log in (/api/sessions)
    [HttpPost]
    public ActionResult<Guid> Login([FromBody] SessionModel sessionModel)
    {
        //TODO: Actually check credentials in sessionLogic and generate token for client (Postman)
        //TODO: Actual token implementation may vary, Guid is used here as default, other could be encode username:password

        var token = new { AccessToken = Guid.NewGuid() };
        return Created(string.Empty, token);
    }

    // Delete - Log out (/api/sessions)
    [HttpDelete]
    public ActionResult Logout([FromHeader] string authorization)
    {
        string authHeader = HttpContext.Request.Headers["Authorization"];

        if (authHeader is null)
            return BadRequest("Missing credentials. Cannot Logout");

        //TODO: Actually search for session and delete if found

        return NoContent();
    }
}
