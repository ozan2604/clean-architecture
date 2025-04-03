using CleanArchitecture.Application.Auth;
using CleanArchitecture.Application.Employees;
using MediatR;
using TS.Result;

namespace WebApi.Modules
{
    public static class AuthModule
    {
        public static void RegisterAuthRoutes(this IEndpointRouteBuilder app)
        {
            RouteGroupBuilder group = app.MapGroup("/auth").WithTags("Auth");

            group.MapPost("login",
                async (ISender sender, LoginCommand request, CancellationToken cancellatioNToken) =>
                {
                    var response = await sender.Send(request, cancellatioNToken);
                    return response.IsSuccessful ? Results.Ok(response) : Results.InternalServerError(response);
                })
                .Produces<Result<LoginCommandResponse>>();

            
        }
    }
}
