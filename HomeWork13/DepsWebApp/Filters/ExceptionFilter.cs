using System;
using DepsWebApp.Mappings;
using DepsWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DepsWebApp.Filters
{
    /// <summary>
    /// Exception filter.
    /// </summary>
    public class ExceptionFilter : ExceptionFilterAttribute, IExceptionFilter
    {
        /// <summary>
        /// OnException handler.
        /// </summary>
        /// <param name="context"></param>
        public override void OnException(ExceptionContext context)
        {
            var error = ExceptionCodeMapper.ToErrorInfo(context.Exception);
            context.Result = new JsonResult(error);
        }
    }
}
