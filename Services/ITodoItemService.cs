using TodoApi.Models;

public interface ITodoItemService
{
    Task<TodoItem> CreateTodoItemAsync(CreateTodoItemDto createTodoItemDto);
    Task<IEnumerable<TodoItem>> GetTodoItemsAsync();
    Task<TodoItem?> GetTodoItemAsync(long id);
    Task<TodoItem?> UpdateTodoItemAsync(long id, UpdateTodoItemDto updateTodoItemDto);
    Task<bool> DeleteTodoItemAsync(long id);
}
