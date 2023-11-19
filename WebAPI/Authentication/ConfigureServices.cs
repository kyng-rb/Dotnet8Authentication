using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using WebAPI.Authentication.Configurations;
using WebAPI.Authentication.Email;
using WebAPI.Authentication.Policy;
using WebAPI.Authentication.TwoFactor;
using WebAPI.Persistence;

namespace WebAPI.Authentication;

public static class ConfigureServices
{
    public static IServiceCollection ConfigureAuth(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthorizationBuilder()
                .AddPolicy(Constants.TrialPolicy, policy =>
                {
                    policy.RequireClaim(Constants.TrialPolicy);
                    policy.Requirements.Add(new TrialPolicyRequirement(0));
                })
                .AddPolicy(Constants.AdminPolicy, policy => { policy.RequireRole("Admin"); });
        services.AddSingleton<IAuthorizationHandler, TrialPolicyHandler>();

        services.Configure<AuthConfigurations>(configuration.GetSection(AuthConfigurations.SectionName));
        services.AddTransient<IEmailSender, CustomEmailSender>();
        services.AddIdentityApiEndpoints<MyUser>(options =>
                {
                    //options.SignIn.RequireConfirmedEmail = true;
                })
                .AddEntityFrameworkStores<ApplicationContext>();
        services.AddSingleton<IGenerateToken, Token>();
        services.AddSingleton<IPolicyManager, PolicyManager>();
        return services;
    }
}