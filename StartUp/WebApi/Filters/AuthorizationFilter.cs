using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using StartUp.IBusinessLogic;
using System;

namespace StartUp.WebApi.Filters
{
    public class AuthorizationFilter : Attribute, IAuthorizationFilter
    {
        private readonly ISessionService _sessionLogic;

        public AuthorizationFilter(ISessionService sessionLogic)
        {
            _sessionLogic = sessionLogic;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //me traigo el token
            var token = context.HttpContext.Request.Headers["Authorization"];

            //verifico que no sea vacia y que sea valida
            if (string.IsNullOrEmpty(token) || !_sessionLogic.ValidateToken(token.ToString()))
            {
                //si no lo es, pido autorizacion para continuar
                context.Result = new JsonResult(new { Message = "Please send your authorization token" })
                { StatusCode = 401 };
            }

            //si el token es valido tengo que permitir la autorizacion dependiendo de que rol tiene ?
            //devuelvo el usuario???
            _sessionLogic.logUser = _sessionLogic.GetUserToken(token);
        }
    }
}
