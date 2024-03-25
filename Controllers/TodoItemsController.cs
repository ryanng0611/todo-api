// using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Middleware;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("todoitem")]
    [ApiController]
    [Authorize]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoItemService _todoItemService;

        public TodoItemsController(ITodoItemService todoItemService)
        {
            _todoItemService = todoItemService;
        }

        // GET: api/TodoItems
        [HttpGet("get-all-items")]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        {
            var todoItems = await _todoItemService.GetTodoItemsAsync();
            return Ok(todoItems);
        }

        // GET: api/TodoItems/5
        [HttpGet("get-single-item/{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(long id)
        {
            var todoItem = await _todoItemService.GetTodoItemAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }

        // PUT: api/TodoItems/5
        [HttpPut("update-item/{id}")]
        public async Task<IActionResult> PutTodoItem(long id, UpdateTodoItemDto updateTodoItemDto)
        {
            var updatedTodoItem = await _todoItemService.UpdateTodoItemAsync(id, updateTodoItemDto);

            if (updatedTodoItem == null)
            {
                return NotFound();
            }

            return Ok(updatedTodoItem);
        }

        // POST: api/TodoItems
        [HttpPost("create-item")]
        public async Task<ActionResult<TodoItem>> PostTodoItem(CreateTodoItemDto newTodoItem)
        {
            var createdTodoItem = await _todoItemService.CreateTodoItemAsync(newTodoItem);

            if (createdTodoItem == null)
            {
                return NotFound();
            }

            return CreatedAtAction(nameof(GetTodoItem), new { id = createdTodoItem.Id }, createdTodoItem);
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("delete-item/{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var deleteSuccess = await _todoItemService.DeleteTodoItemAsync(id);
            if (!deleteSuccess)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
