using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Middleware
{
    public static class RequestMiddlewareExtesion
    {
        /// <summary>
        /// 扩展自定义的
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseRequestMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<RequestMiddlewares>();
        }

    }

    /// <summary>
    /// 自定义中间键
    /// </summary>
    public class RequestMiddlewares
    {
        private readonly RequestDelegate _requestDelegate;
        public RequestMiddlewares(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            //设置中文格式乱码
            //httpContext.Response.ContentType = "text/plain;charset=utf-8";

            //-----判断参数里有H
            if (httpContext.Request.Query.Keys.Contains("Name"))
            {
                await httpContext.Response.WriteAsync("这个url地址中包含了Name的参数");
            }
            //调用下一个管道
            await _requestDelegate(httpContext);

        }

    }
}
