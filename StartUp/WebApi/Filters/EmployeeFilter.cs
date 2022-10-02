using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using StartUp.IBusinessLogic;
using StartUp.Domain.Entities;

namespace StartUp.WebApi.Filters
{
    public class EmployeeFilter : Attribute, IAuthorizationFilter
    {

        public virtual void OnAuthorization(AuthorizationFilterContext context)
        {

            var authorizationHeader = context.HttpContext.Request.Headers["Authorization"];

            if (string.IsNullOrEmpty(authorizationHeader))
            {
                context.Result = new ObjectResult(new { Message = "Authorization header is missing" })
                {
                    StatusCode = 401
                };
            }
            else
            {
                var sessionService = this.GetSessionService(context);
                var authorizationHeaderIsValid = sessionService.IsFormatValidOfAuthorizationHeader(authorizationHeader);
                if (!authorizationHeaderIsValid)
                {
                    context.Result = new ObjectResult(new { Message = "Authorization header format incorrect" })
                    {
                        StatusCode = 401
                    };
                }
                else
                {
                    try
                    {
                        sessionService.AuthenticateAndSaveUser(authorizationHeader);
                        User user = sessionService.UserLogged;
                        if (!user.HasPermissions(new string[] { $"employee".ToLower() }))
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
        }
        protected ISessionService GetSessionService(AuthorizationFilterContext context)
        {
            var sessionHandlerType = typeof(ISessionService);
            var sessionHandlerLogicObject = context.HttpContext.RequestServices.GetService(sessionHandlerType);
            var sessionHandler = sessionHandlerLogicObject as ISessionService;
            return sessionHandler;
        }
    }
}
