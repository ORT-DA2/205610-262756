
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
    private readonly ISessionService _sessionService;
    private readonly IAdministratorService _adminService;
    private readonly IOwnerService _ownerService;
    private readonly IEmployeeService _employeeService;
    private readonly ITokenAccessService _tokenAccessService;

    public SessionController(ISessionService Service, IAdministratorService adminService,
        IOwnerService ownerService, IEmployeeService employeeService, ITokenAccessService tokenService)
    {
        _sessionService = Service;
        _adminService = adminService;
        _ownerService = ownerService;
        _employeeService = employeeService;
        _tokenAccessService = tokenService;
    }
    // Create - Log in (/api/sessions)
    [HttpPost]
    public ActionResult<Guid> Login([FromBody] SessionModel sessionModel)
    {
        //que los campos no sean vacios
        //que exista un usuario con ese username
        //que la password coincida con la password del usuario
        _sessionService.VerifySessionModel(sessionModel);

        User user = _sessionService.GetSpecificUser(sessionModel.UserName);

        Session session = _sessionService.CreateOrRetrieveSession(sessionModel);

        try
        {
            var tokenExist = _tokenAccessService.GetSpecificTokenAccess(session);
            return Created("Successful login", tokenExist);
        }
        catch (ResourceNotFoundException)
        {
            var newToken = _tokenAccessService.CreateTokenAccess(user);

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

        Session session = _sessionService.GetSpecificSession(username);
        _sessionService.VerifySession(session);
        
        _tokenAccessService.DeleteTokenAccess(session);
        _sessionService.Delete(username);
        return Ok("{username} delete");
    }
}
