namespace WebApi.Modules
{
    public static class RouteRegister
    {
        public static void RegisterRoutes(this IEndpointRouteBuilder app)
        {
            app.RegisterEmployeeRoutes();
            app.RegisterAuthRoutes();
        }
    }
}
