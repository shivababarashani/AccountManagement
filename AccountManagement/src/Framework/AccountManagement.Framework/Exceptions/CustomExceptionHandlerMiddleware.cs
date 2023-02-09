﻿using AccountManagement.Framework.ApiResult;
using AccountManagement.Framework.Common;
using AccountManagement.Framework.Logger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;

namespace AccountManagement.Framework.Exceptions
{
    public static class CustomExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
        }
    }

    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context,ILogCoreFactory logger)
        {
            string message = null;
            HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;
            ApiResultStatusCode apiStatusCode = ApiResultStatusCode.ServerError;

            try
            {
                await _next(context);
            }
            catch (AppException exception)
            {
                httpStatusCode = exception.HttpStatusCode;
                apiStatusCode = exception.ApiStatusCode;
               
                //if (_env.IsDevelopment())
                //{
                //    var dic = new Dictionary<string, string>
                //    {
                //        ["Exception"] = exception.Message,
                //        ["StackTrace"] = exception.StackTrace,
                //    };
                //    if (exception.InnerException != null)
                //    {
                //        dic.Add("InnerException.Exception", exception.InnerException.Message);
                //        dic.Add("InnerException.StackTrace", exception.InnerException.StackTrace);
                //    }
                //    if (exception.AdditionalData != null)
                //        dic.Add("AdditionalData", JsonConvert.SerializeObject(exception.AdditionalData));

                //    message = JsonConvert.SerializeObject(dic);
                //}
                //else
                //{
                    message = exception.Message;
                //}
                await WriteToResponseAsync();
            }
            //catch (SecurityTokenExpiredException exception)
            //{
            //    _logger.LogError(exception, exception.Message);
            //    SetUnAuthorizeResponse(exception);
            //    await WriteToResponseAsync();
            //}
            catch (UnauthorizedAccessException exception)
            {
                SetUnAuthorizeResponse(exception);
                await WriteToResponseAsync();
            }
            catch (Exception exception)
            {
                logger.StateLog(LogType.Error, "CustomExceptionHandlerMiddleware", exception.ToString(), exception);

                //if (_env.IsDevelopment())
                //{
                //    var dic = new Dictionary<string, string>
                //    {
                //        ["Exception"] = exception.Message,
                //        ["StackTrace"] = exception.StackTrace,
                //    };
                //    message = JsonConvert.SerializeObject(dic);
                //}
                await WriteToResponseAsync();
            }

            async Task WriteToResponseAsync()
            {
                if (context.Response.HasStarted)
                    throw new InvalidOperationException("The response has already started, the http status code middleware will not be executed.");

                var result = new AccountManagement.Framework.ApiResult.ApiResult(false, apiStatusCode, message);
                var json = JsonConvert.SerializeObject(result);

                context.Response.StatusCode = (int)httpStatusCode;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(json);
            }

            void SetUnAuthorizeResponse(Exception exception)
            {
                httpStatusCode = HttpStatusCode.Unauthorized;
                apiStatusCode = ApiResultStatusCode.UnAuthorized;

                //if (_env.IsDevelopment())
                //{
                //    var dic = new Dictionary<string, string>
                //    {
                //        ["Exception"] = exception.Message,
                //        ["StackTrace"] = exception.StackTrace
                //    };
                //    //if (exception is SecurityTokenExpiredException tokenException)
                //    //    dic.Add("Expires", tokenException.Expires.ToString());

                //    message = JsonConvert.SerializeObject(dic);
                //}
            }
        }
    }
}
