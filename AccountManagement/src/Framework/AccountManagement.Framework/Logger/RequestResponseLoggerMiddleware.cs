using AccountManagement.Framework.Common;
using AccountManagement.Framework.QueueProducer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;


namespace AccountManagement.Framework.Logger
{
    public static class RequestResponseLogMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomRequestResponseLog(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestResponseLogMiddleware>();
        }
    }
    public class RequestResponseLogMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly SiteSettingBaseViewModel _appSetting;
        private readonly IRequestResponseLogger _logger;

        public RequestResponseLogMiddleware(RequestDelegate next,
                                            IOptions<SiteSettingBaseViewModel> options,
                                            IRequestResponseLogger logger)
        {
            _next = next;
            _appSetting = options.Value;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext, IRequestResponseLogModelCreator logCreator)
        {
            RequestResponseLogModel log = logCreator.LogModel;
            // Middleware is enabled only when the EnableRequestResponseLogging config value is set.

            var v = httpContext.TraceIdentifier;
            if (_appSetting == null || !_appSetting.AppSettings.IsRequestLogEnabled)
            {
                await _next(httpContext);
                return;
            }
            log.RequestDateTime = DateTime.UtcNow;
            HttpRequest request = httpContext.Request;


            log.TraceId = httpContext.TraceIdentifier;
            var ip = request.HttpContext.Connection.RemoteIpAddress;
            log.ClientIp = ip == null ? null : ip.ToString();
            log.ApplicationTypeId = _appSetting.AppSettings.ApplicationId;

            var routeData = httpContext.GetRouteData();

            log.RequestMethod = request.Method;
            log.RequestPath = request.Path;
            log.Controller = routeData?.Values["controller"]?.ToString();
            log.Action = routeData?.Values["action"]?.ToString();
            log.Version = routeData?.Values["version"]?.ToString();

            log.RequestQuery = request.QueryString.ToString();
            log.RequestBody = await ReadBodyFromRequest(request);
            log.RequestScheme = request.Scheme;
            log.RequestHost = request.Host.ToString();
            log.UserAgent = request.Headers.Where(p => p.Key == "User-Agent").FirstOrDefault().Value.ToString();
            //ToDo:اینجا حتما شماره سرور رو از گتوی بگیرم
            log.ServerId = _appSetting.AppSettings.ServerId;

            //ToDo:اینجا حتما یوزر رو از گتوی بگیرم
            //log.UserId=???
            HttpResponse response = httpContext.Response;
            var originalResponseBody = response.Body;
            using var newResponseBody = new MemoryStream();
            response.Body = newResponseBody;

            try
            {
                await _next(httpContext);
            }
            catch (Exception exception)
            {
                LogError(log, exception);
            }

            newResponseBody.Seek(0, SeekOrigin.Begin);
            var responseBodyText = await new StreamReader(response.Body).ReadToEndAsync();

            newResponseBody.Seek(0, SeekOrigin.Begin);
            await newResponseBody.CopyToAsync(originalResponseBody);

            log.ExceptionMessage= "";
            log.ExceptionStackTrace ="";

            log.ResponseContentType = response.ContentType;
            log.ResponseStatus = response.StatusCode;
            log.ResponseBody = responseBodyText;
            log.ResponseDateTime = DateTime.UtcNow;
            log.ResponseTime= Convert.ToInt32((log.ResponseDateTime - log.RequestDateTime).TotalMilliseconds);

         
            _logger.Log(logCreator);
        }

        private void LogError(RequestResponseLogModel log, Exception exception)
        {
            log.ExceptionMessage = exception.Message;
            log.ExceptionStackTrace = exception.StackTrace;
        }
        private async Task<string> ReadBodyFromRequest(HttpRequest request)
        {
            request.EnableBuffering();
            using var streamReader = new StreamReader(request.Body, leaveOpen: true);
            var requestBody = await streamReader.ReadToEndAsync();
            request.Body.Position = 0;
            return requestBody;
        }
    }

    public class RequestResponseLogModel
    {
        public Guid UserId { get; set; } 
        public int ApplicationTypeId { get; set; } 
        public string ClientIp { get; set; }
        public string TraceId { get; set; }
        public string UserAgent { get; set; }        
        public int ServerId { get; set; }        
        public DateTime RequestDateTime { get; set; }
        public string RequestMethod { get; set; }
        public string RequestScheme { get; set; }
        public string RequestHost { get; set; }
        public string RequestPath { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Version { get; set; }
        public string RequestQuery { get; set; }
        public string RequestBody { get; set; }


        public DateTime ResponseDateTime { get; set; }
        public int ResponseTime { get; set; }
        public int ResponseStatus { get; set; }
        public string ResponseBody { get; set; }
        public string ResponseContentType { get; set; }


        public string ExceptionMessage { get; set; }
        public string ExceptionStackTrace { get; set; }
    }

    public interface IRequestResponseLogModelCreator
    {
        RequestResponseLogModel LogModel { get; }
        string LogString();
    }

    public interface IRequestResponseLogger
    {
        void Log(IRequestResponseLogModelCreator logCreator);
    }

    public class RequestResponseLogModelCreator : IRequestResponseLogModelCreator
    {
        public RequestResponseLogModel LogModel { get; private set; }

        public RequestResponseLogModelCreator()
        {
            LogModel = new RequestResponseLogModel();
        }

        public string LogString()
        {
            var jsonString = JsonConvert.SerializeObject(LogModel);
            return jsonString;
        }
    }

    public class RequestResponseLogger : IRequestResponseLogger
    {
        private readonly IQueueProducer _queueProducer;
        public RequestResponseLogger(IQueueProducer queueProducer)
        {
            _queueProducer = queueProducer;
        }
        public void Log(IRequestResponseLogModelCreator logCreator)
        {
            _queueProducer.SendMessage("RequestLog", logCreator);
        }
    }
}
