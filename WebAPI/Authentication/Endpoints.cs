using System.Linq;
using System.Security.Claims;
using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using WebAPI.Persistence;

namespace WebAPI.Authentication;

public class Endpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/Fake/Authenticated", () => "Hello World!").RequireAuthorization();
        app.MapGet("/Fake/AuthenticatedWithClaims", 
                   (ClaimsPrincipal user) => $@"Fuck you {user.Identity?.Name} - You are 
                                               Authenticated with ${user.Identity?.AuthenticationType}
                                               Your claims are {string.Join("-", user.Claims)}
                                               Your Identity are {string.Join("-", user.Identities.Select(x => x))}")
           .RequireAuthorization();
        app.MapGet("/Fake/NoAuthenticated", () => "Hello World!");
        app.MapGet("/Fake/Anonymous", () => "Hello World!").AllowAnonymous();

        app.MapGroup("account/").MapIdentityApi<MyUser>();
    }
}