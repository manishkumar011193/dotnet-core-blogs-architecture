using Microsoft.AspNetCore.Builder;

namespace dotnet_core_blogs_architecture.infrastructure
{
    public static class MiddlewareExtensions
    {
        public static void UseRateLimit(this IApplicationBuilder app)
        {
            app.UseMiddleware<RateLimitMiddleware>();
        }
    }
}
