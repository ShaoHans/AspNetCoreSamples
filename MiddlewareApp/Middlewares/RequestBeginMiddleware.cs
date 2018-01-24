using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MiddlewareApp.Middlewares
{
    public class RequestBeginMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestBeginMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext context)
        {
            context.Items.Add("StartTime", DateTime.Now);
            return _next.Invoke(context);
        }
    }

    public static class RequestBeginMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequesetBegin(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestBeginMiddleware>();
        }
    }
}
