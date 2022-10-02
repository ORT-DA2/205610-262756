using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using StartUp.IBusinessLogic;
using StartUp.Domain.Entities;

namespace StartUp.WebApi.Filters
{
    public class AdministratorFilter : Attribute, IAuthorizationFilter
    {

        public virtual void OnAuthorization(AuthorizationFilterContext context)
        {

            var authorizationHeader = context.HttpContext.Request.Headers["Authorization"];
            var sessionService = this.GetSessionService(context);

            if (sessionService.UserLogged == null)
            {
                context.Result = new ObjectResult(new { Message = "Authorization header is missing" })
                {
                    StatusCode = 401
                };
            }
            else
            {
                TokenAccess token = sessionService.GetTokenUser();

                try
                {
                    sessionService.AuthenticateAndSaveUser(authorizationHeader);

                    if (!token.User.HasPermissions(new string[] { $"administrator".ToLower() }))
                    {
                        context.Result = new ObjectResult(new { Message = "no tiene los permissssos", asdas = "asd" })
                        {
                            StatusCode = 403
                        }; ;
                    }
                }
                catch (Exception)
                {
                    context.Result = new ObjectResult(new { Message = "Please login to continue" })
                    {
                        StatusCode = 401
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
