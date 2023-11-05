namespace WebAPI.Authentication.Configurations;

public class AuthConfigurations
{
    public const string SectionName = "Security";
    public required AuthEmail Email { get; set; }
}