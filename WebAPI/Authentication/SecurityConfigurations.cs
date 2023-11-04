namespace WebAPI.Authentication;

public class SecurityConfigurations
{
    public const string SectionName = "Security";
    public required SecurityEmail Email { get; set; }
}