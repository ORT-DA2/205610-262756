
using Domain;
using IBusinessLogic;
using Microsoft.AspNetCore.Mvc;
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
    private readonly ISessionManager _sessionManager;
    private readonly IAdministratorManager _adminManager;
    private readonly IOwnerManager _ownerManager;
    private readonly IEmployeeManager _employeeManager;
    private readonly ITokenAccessManager _tokenAccessManager;

    public SessionController(ISessionManager manager, IAdministratorManager adminManager,
        IOwnerManager ownerManager, IEmployeeManager employeeManager, ITokenAccessManager tokenManager)
    {
        _sessionManager = manager;
        _adminManager = adminManager;
        _ownerManager = ownerManager;
        _employeeManager = employeeManager;
        _tokenAccessManager = tokenManager;
    }
    // Create - Log in (/api/sessions)
    [HttpPost]
    public ActionResult<Guid> Login([FromBody] SessionModel sessionModel)
    {
        //que los campos no sean vacios
        //que exista un usuario con ese username
        //que la password coincida con la password del usuario
        _sessionManager.VerifySessionModel(sessionModel);

        User user = _sessionManager.GetSpecificUser(sessionModel.UserName);

        Session session = _sessionManager.CreateOrRetrieveSession(sessionModel);

        try
        {
            var tokenExist = _tokenAccessManager.GetSpecificTokenAccess(session);
            return Created("Successful login", tokenExist);
        }
        catch (ResourceNotFoundException)
        {
            var newToken = _tokenAccessManager.CreateTokenAccess(user);

            return Created("Successful login", newToken);
        }

    }


    // Delete - Log out (/api/sessions/{username})
    [HttpDelete("{username}")]
    [RolFilter("administrator")]
    public ActionResult Logout([FromHeader] string authorization, string username)
    {
        if(string.IsNullOrEmpty(authorization) || string.IsNullOrEmpty(username))
        {
            return BadRequest();
        }
        string authHeader = HttpContext.Request.Headers["Authorization"];

        if (authHeader is null)
            return BadRequest("Missing credentials. Cannot Logout");

        Session session = _sessionManager.GetSpecificSession(username);
        _sessionManager.VerifySession(session);
        
        _tokenAccessManager.DeleteTokenAccess(session);
        _sessionManager.Delete(username);
        return Ok("{username} delete");
    }
}
