namespace TodoApi.Models;

public class CreateUserDto
{
    public string DisplayName { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
}