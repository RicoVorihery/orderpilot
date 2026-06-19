namespace StockDemo.Api.Middleware
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private const string HEADER_NAME = "x-api-key";
        private const string CONFIG_KEY = "ApiSettings:ApiKey";
        public ApiKeyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/scalar") ||
            context.Request.Path.StartsWithSegments("/openapi"))
            {
                await _next(context);
                return;
            }

            if (!context.Request.Headers.TryGetValue(HEADER_NAME, out var extractedApiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("API Key was not provided");
                return;
            }

            var apiKey = context.RequestServices
                .GetRequiredService<IConfiguration>()
                .GetValue<string>(CONFIG_KEY);

            if (String.IsNullOrEmpty(apiKey) || !apiKey.Equals(extractedApiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Unauthorized client.");
                return;
            }

            await _next(context);
        }
    }
}