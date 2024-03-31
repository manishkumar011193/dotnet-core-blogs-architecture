using dotnet_core_blogs_architecture.Data.RateLimiter;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet_core_blogs_architecture.Data
{
    public static class MiddlewareExtensions
    {
        public static void UseRateLimit(this IApplicationBuilder app)
        {
            app.UseMiddleware<RateLimitMiddleware>();
        }
    }
}
