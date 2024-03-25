namespace TodoApi.Models;

public class CreateTodoItemDto
{
    public string? Name { get; set; }
    public Guid UserId { get; set; }
}