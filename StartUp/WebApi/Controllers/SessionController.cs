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
    private readonly IUserService _userService;

    public SessionController(ISessionService sessionService, ITokenAccessService tokenService, IUserService userService)
    {
        _sessionService = sessionService;
        _tokenAccessService = tokenService;
        _userService = userService;
    }

    [HttpPost]
    public ActionResult<Guid> Login([FromBody] SessionModel sessionModel)
    {
        User user = _sessionService.VerifySession(sessionModel);

        Session session = _sessionService.CreateOrRetrieveSession(sessionModel);

        try
        {
            var tokenExist = _tokenAccessService.GetSpecificTokenAccess(session);
            _userService.SaveToken(user, tokenExist.Token.ToString());

            return Created("Successful login", tokenExist);
        }
        catch (ResourceNotFoundException)
        {
            var newToken = _tokenAccessService.CreateTokenAccess(user);
            _userService.SaveToken(user, newToken.Token.ToString());

            return Created("Successful login", newToken);
        }
    }


    [HttpDelete("{username}")]
    public ActionResult Logout([FromHeader] string authorization, string username)
    {
        if (string.IsNullOrEmpty(authorization) || string.IsNullOrEmpty(username))
        {
            return BadRequest();
        }

        Session session = _sessionService.GetSpecificSession(username);
        _sessionService.VerifySession(new SessionModel(session));

        _tokenAccessService.DeleteTokenAccess(session);

        return Ok();
    }
}
