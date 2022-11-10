using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using StartUp.IBusinessLogic;
using StartUp.Domain.Entities;
using StartUp.Exceptions;

namespace StartUp.WebApi.Filters
{
    public class AuthorizationFilter : Attribute, IAuthorizationFilter
    {
        private string _role;
        public AuthorizationFilter(string role = "")
        {
            _role = role;
        }

        public virtual void OnAuthorization(AuthorizationFilterContext context)
        {

            var authorizationHeader = context.HttpContext.Request.Headers["Authorization"];
            var sessionService = this.GetSessionService(context);

            if (string.IsNullOrEmpty(authorizationHeader))
            {
                context.Result = new ObjectResult(new { Message = "Login or enter a valid token to continue" })
                {
                    StatusCode = 401
                };
            }
            else
            {
                if (authorizationHeader.ToString().Contains("Bearer "))
                {
                    authorizationHeader = sessionService.CleanAuthorization(authorizationHeader);
                }
                try
                {
                    User user = sessionService.GetTokenUser(authorizationHeader);

                    try
                    {
                        if (!user.HasPermissions(new string[] { $"{_role}" }))
                        {
                            context.Result = new ObjectResult(new { Message = "You do not have the permissions to perform the action" })
                            {
                                StatusCode = 403
                            }; ;
                        }
                        sessionService.UserLogged = user;
                    }
                    catch (Exception)
                    {
                        context.Result = new ObjectResult(new { Message = "Please login to continue" })
                        {
                            StatusCode = 401
                        };
                    }
                }
                catch (InputException ex)
                {
                    context.Result = new ObjectResult(new { Message = ex.Message})
                    {
                        StatusCode = 404
                    };
                }
            }
        }

        protected ISessionService GetSessionService(AuthorizationFilterContext context)
        {
            var sessionManagerType = typeof(ISessionService);
            object sessionManagerObject = context.HttpContext.RequestServices.GetService(sessionManagerType);
            var sessionManager = (ISessionService)sessionManagerObject;
            return sessionManager;
        }
    }
}
