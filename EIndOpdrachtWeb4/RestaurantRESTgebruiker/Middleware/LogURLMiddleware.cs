namespace RestaurantRESTgebruiker.Middleware
{
    public class LogURLMiddleware
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;

        public LogURLMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<LogURLMiddleware>();
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            finally
            {
                _logger.LogInformation("Request {method} {url} => {statuscode}",
                    context.Request?.Method,
                    context.Request?.Path.Value,
                    context.Response?.StatusCode);
            }
        }
    }
}
