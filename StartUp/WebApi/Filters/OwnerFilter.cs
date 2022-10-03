using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using StartUp.IBusinessLogic;
using StartUp.Domain.Entities;

namespace StartUp.WebApi.Filters
{
    public class OwnerFilter : Attribute, IAuthorizationFilter
    {

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
                    authorizationHeader = CleanAuthorization(authorizationHeader);
                }
                User user = sessionService.GetTokenUser(authorizationHeader);

                try
                {
                    if (!user.HasPermissions(new string[] { $"owner".ToLower() }))
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

        private string CleanAuthorization(string authorizationHeader)
        {
            string authorization = "";
            for (int i = 7; i < authorizationHeader.Length; i++)
            {
                authorization = authorization + authorizationHeader[i];
            }
            return authorization;
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