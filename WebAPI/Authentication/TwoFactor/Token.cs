using System.Security.Claims;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using WebAPI.Persistence;

namespace WebAPI.Authentication.TwoFactor;

public class Token : IGenerateToken
{
    public async Task<Result<string>> GetTwoFactorToken(ClaimsPrincipal principal, IServiceProvider serviceProvider)
    {
        var signInManager = serviceProvider.GetRequiredService<SignInManager<MyUser>>();
        var userManager = signInManager.UserManager;
        if (await userManager.GetUserAsync(principal) is not { } user)
        {
            return Result.Fail("Invalid");
        }

        var token =
            await userManager.GenerateTwoFactorTokenAsync(user, userManager.Options.Tokens.AuthenticatorTokenProvider);

        return Result.Ok(token);
    }
}