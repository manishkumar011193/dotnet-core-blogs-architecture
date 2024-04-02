using Microsoft.Extensions.Caching.Distributed;
using Microsoft.AspNetCore.Http;
namespace dotnet_core_blogs_architecture.infrastructure
{
    public class RateLimitMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IDistributedCache _cache;
        private readonly TimeSpan _timeWindow;
        private readonly int _maxRequests;

        public RateLimitMiddleware(RequestDelegate next, IDistributedCache cache)
        {
            _next = next;
            _cache = cache;
            _timeWindow = TimeSpan.FromMinutes(1);
            _maxRequests = 100;
        }
        public async Task InvokeAsync(HttpContext context, IDistributedCache cache)
        {
            try
            {
                var ipAddress = context.Connection.RemoteIpAddress.ToString();
                var cacheKey = $"RateLimit:{ipAddress}";

                var requestCount = await _cache.GetStringAsync(cacheKey);
                if (requestCount == null)
                {
                    requestCount = "1";
                    var options = new DistributedCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = _timeWindow
                    };
                    await _cache.SetStringAsync(cacheKey, requestCount, options);
                }
                else
                {
                    var count = int.Parse(requestCount);
                    if (count >= _maxRequests)
                    {
                        context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                        await context.Response.WriteAsync("Rate limit exceeded. Try again later.");
                        return;
                    }
                    else
                    {
                        count++;
                        await _cache.SetStringAsync(cacheKey, count.ToString());
                    }
                }
                await _next(context);
            }
            catch(Exception ex)
            {
              
            }
         
        }
    }
}
