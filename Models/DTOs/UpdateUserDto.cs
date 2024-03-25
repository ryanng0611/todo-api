namespace TodoApi.Models;
public class UpdateUserDto
{
    public string? DisplayName { get; set; }
    public string? UserName { get; set; }
    public string? Password { get; set; }
}