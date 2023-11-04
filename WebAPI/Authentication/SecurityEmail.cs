namespace WebAPI.Authentication;

public class SecurityEmail
{
    public required string Address { get; set; }
    public required string Host { get; set; }
    public required string Password { get; set; }
    public required int Port { get; set; }
}