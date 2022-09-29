﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using StartUp.IBusinessLogic;
using System;

namespace StartUp.WebApi.Filters
{
    public class AuthorizationFilter : Attribute, IAuthorizationFilter
    {
        private readonly ISessionManager _sessionLogic;

        public AuthorizationFilter(ISessionManager sessionLogic)
        {
            _sessionLogic = sessionLogic;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var token = context.HttpContext.Request.Headers["Authorization"];


            if (string.IsNullOrEmpty(token) && _sessionLogic.ValidateToken())
            {
                // Corto la ejecucion de la request cuando asigno un result
                context.Result = new JsonResult(new { Message = "Please send your authorization token" })
                { StatusCode = 401 };
            }
        }
    }
}
