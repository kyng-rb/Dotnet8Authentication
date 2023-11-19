using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Authentication.Policy;

public class TrialPolicyRequirement : IAuthorizationRequirement
{
    public TrialPolicyRequirement(int days)
    {
        ExpireInDays = days;
    }

    public int ExpireInDays { get; init; }
}