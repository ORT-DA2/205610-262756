using IBusinessLogic;
using Microsoft.AspNetCore.Mvc;
using StartUp.Domain.Entities;
using StartUp.Exceptions;
using StartUp.IBusinessLogic;
using StartUp.Models.Models.In;
using System;
using System.Collections.Generic;
using StartUp.Models.Models.Out;

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
        try
        {
            if (authorization.ToString().Contains("Bearer "))
            {
                authorization = _sessionService.CleanAuthorization(authorization);
            }
            
            Session session = _sessionService.GetSpecificSession(username);
            User user = _sessionService.VerifySession(new SessionModel(session));

            if (user.Token == authorization)
            {
                user.Token = "";
                _userService.SaveToken(user, user.Token);
                _sessionService.DeleteSession(username);

                return Ok("Closed session");
            }
            else
            {
                return BadRequest("The authorization does not correspond to the session that you want to close");
            }
        }
        catch (InputException)
        {
            return BadRequest("Login first");
        }
    }
}
