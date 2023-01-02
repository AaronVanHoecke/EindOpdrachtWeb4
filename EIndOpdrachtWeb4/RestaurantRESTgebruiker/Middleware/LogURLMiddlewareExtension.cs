namespace RestaurantRESTgebruiker.Middleware
{
    public static class LogURLMiddlewareExtension
    {
        public static IApplicationBuilder UseLogURLMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LogURLMiddleware>();
        }
    }
}
