using IBusinessLogic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using StartUp.BusinessLogic;
using StartUp.Domain.Entities;
using StartUp.Exceptions;
using StartUp.IBusinessLogic;
using StartUp.Models.Models.In;
using StartUp.WebApi.Filters;
using System;

namespace StartUp.WebApi.Controllers;

[Route("api/sessions")]
[ApiController]
public class SessionController : ControllerBase
{
    private readonly ISessionService _sessionService;
    private readonly ITokenAccessService _tokenAccessService;

    public SessionController(ISessionService sessionService, ITokenAccessService tokenService)
    {
        _sessionService = sessionService;
        _tokenAccessService = tokenService;
    }
    // Create - Log in (/api/sessions)
    [HttpPost]
    public ActionResult<Guid> Login([FromBody] SessionModel sessionModel)
    {
        _sessionService.VerifySessionModel(sessionModel);

        User user = _sessionService.GetSpecificUser(sessionModel.UserName);

        Session session = _sessionService.CreateOrRetrieveSession(sessionModel);

        try
        {
            var tokenExist = _tokenAccessService.GetSpecificTokenAccess(session);
            _sessionService.UserLogged = tokenExist.User;

            return Created("Successful login", tokenExist);
        }
        catch (ResourceNotFoundException)
        {
            var newToken = _tokenAccessService.CreateTokenAccess(user);
            _sessionService.UserLogged = newToken.User;

            return Created("Successful login", newToken);
        }

    }


    // Delete - Log out (/api/sessions/{username})
    [HttpDelete("{username}")]
    //[RolFilter("User")]
    public ActionResult Logout([FromHeader] string authorization, string username)
    {
        if (string.IsNullOrEmpty(authorization) || string.IsNullOrEmpty(username))
        {
            return BadRequest();
        }

        Session session = _sessionService.GetSpecificSession(username);
        _sessionService.VerifySession(session);
        
        _tokenAccessService.DeleteTokenAccess(session);

        return Ok();
    }
}
