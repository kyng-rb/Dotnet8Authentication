using System.Security.Claims;
using Carter;
using WebAPI.Authentication.Policy;
using WebAPI.Authentication.TwoFactor;
using WebAPI.Persistence;

namespace WebAPI.Authentication;

public class Endpoints : ICarterModule
{
    private readonly IPolicyManager _policy;
    private readonly IGenerateToken _twoFactorToken;

    public Endpoints(IGenerateToken twoFactorToken, IPolicyManager policy)
    {
        _twoFactorToken = twoFactorToken;
        _policy = policy;
    }

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/Fake/Authenticated", () => "Hello World!").RequireAuthorization(Constants.TrialPolicy);
        app.MapGet("/Fake/AuthenticatedWithClaims",
                   (ClaimsPrincipal user) => $@"
                                               Fuck you {user.Identity?.Name} - You are 
                                               Authenticated with ${user.Identity?.AuthenticationType}
                                               Your claims are {string.Join("-", user.Claims)}
                                               Your Identity are {string.Join("-", user.Identities.Select(x => x))}")
           .RequireAuthorization();
        app.MapGet("/Fake/NoAuthenticated", () => "Hello World!");
        app.MapGet("/Fake/Anonymous", () => "Hello World!")
           .AllowAnonymous();

        app.MapGroup("account/").MapIdentityApi<MyUser>();
        app.MapGet("TwoFactor", (ClaimsPrincipal principal, IServiceProvider provider) =>
                       _twoFactorToken.GetTwoFactorToken
                           (principal, provider))
           .RequireAuthorization();

        app.MapPost("account/trial", (ClaimsPrincipal principal, IServiceProvider provider) => _policy.AddTrialPolicy
                    (principal, provider))
           .RequireAuthorization();
    }
}