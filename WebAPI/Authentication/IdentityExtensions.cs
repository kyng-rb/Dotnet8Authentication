using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebAPI.Persistence;

namespace WebAPI.Authentication;

public static class IdentityExtensions
{
    public static IServiceCollection ConfigureAuth(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthorization();
        
        services.Configure<SecurityConfigurations>(configuration.GetSection(SecurityConfigurations.SectionName));
        services.AddTransient<IEmailSender, CustomEmailSender>();

        services.AddIdentityApiEndpoints<MyUser>(options => options.SignIn.RequireConfirmedEmail = true)
                .AddEntityFrameworkStores<ApplicationContext>();

        return services;
    }
}