using System;
using System.Security.Authentication;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using API.Common.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace API.Common.Exceptions
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = GetExceptionResponse(exception);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = response.Code;

            return context.Response.WriteAsync(response.ToString());
        }

        private ExceptionResponse GetExceptionResponse(Exception exception)
        {
            // if method is increased, it makes sense to consider creating objects dynamically with reflection.

            ExceptionResponse response;

            response = new ExceptionResponse(exception);
            switch (exception)
            {
                case DbUpdateException updateException when updateException.InnerException != null:
                    response.Error = GetDbUpdateExceptionMessage(updateException);
                    break;
                case InvalidCredentialException _:
                    response.Error = ErrorMessages.UserNotExistsOrPasswordIsWrong;
                    break;
                case UnauthorizedAccessException _:
                    response.Error = ErrorMessages.UserIsNotAuthorized;
                    response.Code = 401;
                    break;
            }

            return response;
        }

        private string GetDbUpdateExceptionMessage(DbUpdateException exception)
        {
            if (exception.InnerException == null)
            {
                return null;
            }

            var fullMessage = exception.InnerException.Message;
            var value = Regex.Match(fullMessage, "(?<=\\().+?(?=\\))");
            
            return string.Format(ErrorMessages.SomeValueIsNotUnique, value);
        }
    }
}
