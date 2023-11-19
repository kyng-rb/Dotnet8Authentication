using System.Security.Claims;
using FluentResults;

namespace WebAPI.Authentication.Policy;

public interface IPolicyManager
{
    Task<Result> AddTrialPolicy(ClaimsPrincipal userPrincipal, IServiceProvider serviceProvider);
}