using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace WebCalendar.WebApi.Middleware
{
    public class LogHttpContextMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LogHttpContextMiddleware> _log;

        public LogHttpContextMiddleware(ILogger<LogHttpContextMiddleware> log, RequestDelegate next)
        {
            _log = log;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            string requestMethod = context.Request.Method;
            string userIp = context.Connection.RemoteIpAddress.ToString();
            string requestUrl = context.Request.Path;

            if (requestUrl.StartsWith("/api"))
            {
                _log.LogInformation($"Method: {requestMethod}, user IP: {userIp}, url: {requestUrl}");
            }

            await _next.Invoke(context);
        }
    }
}