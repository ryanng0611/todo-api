namespace TodoApi.Models;

public class AuthenticateResponseDto
{
    public Guid UserId { get; set; }
    public string? Username { get; set; }
    public UserRole Role { get; set; }
    public string Token { get; set; }

    public AuthenticateResponseDto(User user, string token)
    {
        UserId = user.UserId;
        Username = user.UserName;
        Role = user.Role;
        Token = token;
    }
}
