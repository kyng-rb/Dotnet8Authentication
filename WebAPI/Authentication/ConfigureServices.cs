using Microsoft.AspNetCore.Identity.UI.Services;
using WebAPI.Authentication.Configurations;
using WebAPI.Authentication.Email;
using WebAPI.Persistence;

namespace WebAPI.Authentication;

public static class ConfigureServices
{
    public static IServiceCollection ConfigureAuth(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthorization();

        services.Configure<AuthConfigurations>(configuration.GetSection(AuthConfigurations.SectionName));
        services.AddTransient<IEmailSender, CustomEmailSender>();

        services.AddIdentityApiEndpoints<MyUser>(options => options.SignIn.RequireConfirmedEmail = true)
                .AddEntityFrameworkStores<ApplicationContext>();

        return services;
    }
}