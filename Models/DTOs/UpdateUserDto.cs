namespace TodoApi.Models;
public class UpdateUserDto
{
    public Guid UserId { get; set; }
    public string? DisplayName { get; set; }
    public string? UserName { get; set; }
    public string? Password { get; set; }
}