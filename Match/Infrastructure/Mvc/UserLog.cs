using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Match.Entities;
using Match.Services;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Match.Infrastructure
{
    public class UserLog : Microsoft.AspNetCore.Mvc.Filters.IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            string currentUserId = "0";
            var requestPath = context.HttpContext.Request.Path;
            var resultContext = await next();
            if (resultContext.HttpContext.User.Identity.IsAuthenticated)
            {
                currentUserId = resultContext.HttpContext.User
                    .FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value;
            }

            //SysUserLog log = new SysUserLog()
            //{
            //    UserId = currentUserId,
            //    Refer = resultContext.ActionDescriptor.AttributeRouteInfo.ToString(),
            //    Destination = resultContext.HttpContext.Request.Path,
            //    QueryString = resultContext.HttpContext.Request.QueryString.ToString(),
            //    Method = resultContext.HttpContext.Request.Method,
            //    IpAddress = resultContext.HttpContext.Request.Host.Value,
            //    RequestTime = System.DateTime.Now
            //};

            //var service = resultContext.HttpContext.RequestServices.GetService<IMemberService>();
            //await service.AddUserLogAsync(log, currentUserId);

        }
    }
}
