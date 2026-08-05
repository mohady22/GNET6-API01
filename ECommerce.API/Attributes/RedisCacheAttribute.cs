using ECommerce.Application.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;

namespace ECommerce.API.Attributes
{
    public class RedisCacheAttribute:ActionFilterAttribute
    {
        private readonly int _durationInSec;
        public RedisCacheAttribute(int durationInSec)
        {
            _durationInSec = durationInSec;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();
            var cacheKey = CreateCacheKey(context.HttpContext.Request);
            var cached = await cacheService.GetAsync(cacheKey);

            if (!string.IsNullOrEmpty(cached))
            {
                context.Result = new ContentResult()
                {
                    Content = cached,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK,
                };
                return;
            }
            var executed = await next.Invoke();
            if(executed.Result is OkObjectResult { Value:not null } ok)
                await cacheService.SetAsync(cacheKey, ok.Value,TimeSpan.FromSeconds(_durationInSec));

            return;
        }
        private static string CreateCacheKey(HttpRequest request)
        {
            var key = new StringBuilder();
            key.Append(request.Path).Append("?");

            foreach (var (k, v) in request.Query.OrderBy(Q => Q.Key))
            {
                key.Append(k).Append("=").Append(v).Append("&");
            }
            return key.ToString();  
        }
    }
}
