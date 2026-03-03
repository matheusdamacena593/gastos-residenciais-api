using GastosResidenciais.Communication.Responses;
using GastosResidenciais.Exception;
using GastosResidenciais.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GastosResidenciais.Api.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<ExceptionFilter> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionFilter(ILogger<ExceptionFilter> logger, IHostEnvironment env)
        {
            _logger = logger;
            _env = env;
        }

        public void OnException(ExceptionContext context)
        {
            var root = context.Exception.GetBaseException();

            _logger.LogError(
                context.Exception,
                "Unhandled exception. TraceId: {TraceId} Path: {Path} Method: {Method} Root: {RootMessage}",
                context.HttpContext.TraceIdentifier,
                context.HttpContext.Request.Path,
                context.HttpContext.Request.Method,
                root.Message
            );

            if (context.Exception is GastosResidenciaisException)
            {
                HandleProjectException(context);
            }
            else
            {
                ThrowUnknowError(context);
            }
        }

        private void HandleProjectException(ExceptionContext context)
        {
            var cashFlowException = (GastosResidenciaisException)context.Exception;

            _logger.LogWarning(context.Exception, "Project exception: {Message}", cashFlowException.Message);

            var erroResponse = new ResponseErrorsJson(cashFlowException.GetErrors());

            context.HttpContext.Response.StatusCode = cashFlowException.StatusCode;
            context.Result = new ObjectResult(erroResponse);
        }

        private void ThrowUnknowError(ExceptionContext context)
        {
            if (_env.IsDevelopment())
            {
                var root = context.Exception.GetBaseException();
                var erroResponseDev = new ResponseErrorsJson(root.Message);

                context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Result = new ObjectResult(erroResponseDev);
                return;
            }

            var erroResponse = new ResponseErrorsJson(ResourceErrorMessages.UNKNOWN_ERROR);
            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Result = new ObjectResult(erroResponse);
        }
    }
}
