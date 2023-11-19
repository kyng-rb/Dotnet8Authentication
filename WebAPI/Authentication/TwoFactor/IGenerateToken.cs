using System.Security.Claims;
using FluentResults;

namespace WebAPI.Authentication.TwoFactor;

public interface IGenerateToken
{
    Task<Result<string>> GetTwoFactorToken(ClaimsPrincipal principal, IServiceProvider sp);
}