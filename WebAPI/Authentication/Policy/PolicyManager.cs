using System.Security.Claims;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using WebAPI.Persistence;

namespace WebAPI.Authentication.Policy;

public class PolicyManager : IPolicyManager
{
    public async Task<Result> AddTrialPolicy(ClaimsPrincipal userPrincipal, IServiceProvider serviceProvider)
    {
        SignInManager<MyUser> signInManager = serviceProvider.GetRequiredService<SignInManager<MyUser>>();
        UserManager<MyUser> userManager = signInManager.UserManager;
        if (await userManager.GetUserAsync(userPrincipal) is not { } user) return Result.Fail("Invalid");

        IList<Claim> claims = await userManager.GetClaimsAsync(user).ConfigureAwait(false);
        if (claims.Any(claim => claim.Type == Constants.TrialPolicy))
            return Result.Fail("Claim already exists");

        Claim claim = new(Constants.TrialPolicy, DateTime.UtcNow.ToShortDateString());
        await userManager.AddClaimAsync(user, claim).ConfigureAwait(false);

        return Result.Ok();
    }
}