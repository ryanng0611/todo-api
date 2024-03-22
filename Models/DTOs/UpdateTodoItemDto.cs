namespace TodoApi.Models;

public class UpdateTodoItemDto
{
    public string? Name { get; set; }
    public bool IsComplete { get; set; }
}