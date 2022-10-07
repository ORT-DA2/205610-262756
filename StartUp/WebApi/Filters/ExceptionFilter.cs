using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using StartUp.Exceptions;
using System;

namespace StartUp.WebApi.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            try
            {
                throw context.Exception;
            }
            catch (ResourceNotFoundException e)
            {
                context.Result = new JsonResult(e.Message) { StatusCode = 404 };
            }
            catch (InvalidResourceException e)
            {
                context.Result = new JsonResult(e.Message) { StatusCode = 400 };
            }
            catch (InputException e)
            {
                context.Result = new JsonResult(e.Message) { StatusCode = 400 };
            }
            catch (Exception e)
            {
                // TODO: Loggear
                context.Result = new JsonResult(e.Message) { StatusCode = 500 };
            }
        }
    }
}
