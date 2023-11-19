using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Authentication.Policy;

public class TrialPolicyHandler
    : AuthorizationHandler<TrialPolicyRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                   TrialPolicyRequirement requirement)
    {
        ClaimsPrincipal user = context.User;

        if (!user.HasClaim(c => c.Type == Constants.TrialPolicy))
            return Task.CompletedTask;

        string claimValue = user.Claims.First(claim => claim.Type.Equals(Constants.TrialPolicy)).Value;

        DateOnly registeredAt = DateOnly.Parse(claimValue);

        if (registeredAt.AddDays(requirement.ExpireInDays).Day > DateTime.UtcNow.Day)
            context.Succeed(requirement);

        return Task.CompletedTask;
    }
}