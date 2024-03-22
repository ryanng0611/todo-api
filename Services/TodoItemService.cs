using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
public class TodoItemService : ITodoItemService
{
    private readonly TodoContext _context;

    public TodoItemService(TodoContext context)
    {
        _context = context;
    }

    public async Task<TodoItem> CreateTodoItemAsync(CreateTodoItemDto createTodoItemDto)
    {
        var newTodoItem = new TodoItem
        {
            Name = createTodoItemDto.Name,
            IsComplete = false
        };

        _context.TodoItems.Add(newTodoItem);
        await _context.SaveChangesAsync();

        return newTodoItem;
    }

    public async Task<IEnumerable<TodoItem>> GetTodoItemsAsync()
    {
        return await _context.TodoItems.ToListAsync();
    }

    public async Task<TodoItem?> GetTodoItemAsync(long id)
    {
        return await _context.TodoItems.FindAsync(id);
    }

    public async Task<TodoItem?> UpdateTodoItemAsync(long id, UpdateTodoItemDto updateTodoItemDto)
    {
        var todoItem = await _context.TodoItems.FindAsync(id);
        if (todoItem == null)
        {
            return null;
        }

        todoItem.Name = updateTodoItemDto.Name ?? todoItem.Name;
        todoItem.IsComplete = updateTodoItemDto.IsComplete;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!TodoItemExists(id))
            {
                return null;
            }
            else
            {
                throw;
            }
        }

        return todoItem;
    }

    public async Task<bool> DeleteTodoItemAsync(long id)
    {
        var todoItemToDelete = await _context.TodoItems.FindAsync(id);
        if (todoItemToDelete == null)
        {
            return false;
        }

        _context.TodoItems.Remove(todoItemToDelete);
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!TodoItemExists(id))
            {
                return false;
            }
            else
            {
                throw;
            }

        }

        return true;
    }


    private bool TodoItemExists(long id)
    {
        return _context.TodoItems.Any(e => e.Id == id);
    }
}