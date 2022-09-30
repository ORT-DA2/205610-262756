using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using StartUp.Domain.Entities;
using StartUp.IBusinessLogic;
using System;

namespace StartUp.WebApi.Filters
{

    public class RolFilter : Attribute, IAuthorizationFilter
    {
        private readonly string _rol;

        public RolFilter(string rol)
        {
            _rol = rol;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string valueAuthorization = context.HttpContext.Request.Headers["Authorization"];
            var sessionManager = GetSessionForContext(context);
            var loggedUser = GetLoggedUser(context);
            if (!string.IsNullOrEmpty(valueAuthorization))
            {
                try
                {
                    User userLogged = sessionManager.GetSpecificUser(loggedUser.user.Invitation.UserName);
                    if (!(userLogged is null))
                    {
                        bool permission = sessionManager.Permission(userLogged, _rol);

                        if (!permission)
                        {
                            context.Result = new JsonResult(new { Message = "You do not have authorization to perform this action" })
                            { StatusCode = 401 };
                        }
                        else
                        {
                            loggedUser.user = userLogged;
                        }
                    }
                    else
                    {
                        context.Result = new JsonResult(new { Message = "Invalid authorization header. A user with this token does not exist" })
                        { StatusCode = 401 };
                    }
                }
                catch
                {
                    throw new Exception("no tenes autorizacion de usuario");
                }
            }
        }

        private ISessionService GetSessionForContext(AuthorizationFilterContext context)
        {
            var sessionManagerType = typeof(ISessionService);
            object sessionManagerObject = context.HttpContext.RequestServices.GetService(sessionManagerType);
            var sessionManager = (ISessionService)sessionManagerObject;
            return sessionManager;
        }

        private LoggedUser GetLoggedUser(AuthorizationFilterContext context)
        {
            var loggedUserType = typeof(LoggedUser);
            object loggedUserObject = context.HttpContext.RequestServices.GetService(loggedUserType);
            var loggedUser = (LoggedUser)loggedUserObject;
            return loggedUser;
        }
    }
}
