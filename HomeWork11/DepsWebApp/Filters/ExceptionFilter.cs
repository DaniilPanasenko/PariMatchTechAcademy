using System;
using DepsWebApp.Mappings;
using DepsWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DepsWebApp.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute, IExceptionFilter
    {

        public override void OnException(ExceptionContext context)
        {
            var error = ExceptionCodeMapper.ToErrorInfo(context.Exception);
            context.Result = new JsonResult(error);
        }
    }
}
