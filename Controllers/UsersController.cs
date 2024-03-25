using Microsoft.AspNetCore.Mvc;
using TodoApi.Middleware;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("user")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser(LoginUserDto loginUserDto)
        {
            var response = await _userService.LoginUser(loginUserDto);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpPost("create-user")]
        [Authorize]
        public async Task<ActionResult> CreateNewUser(CreateUserDto createUserDto)
        {
            var createdNewUser = await _userService.CreateUserAsync(createUserDto);

            if (createdNewUser == null)
            {
                return Conflict("User with this username already exists");
            }

            return Ok("User registered successfully.");
        }

        [HttpGet("get-all-users")]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            var userList = await _userService.GetUsersAsync();
            return Ok(userList);
        }

        [HttpGet("get-user")]
        public async Task<ActionResult<User>> GetUser(Guid userId)
        {
            var user = await _userService.GetUserAsync(userId);
            if (user == null)
            {
                return NotFound($"User with id ({userId}) was not found.");
            }
            return Ok(user);
        }

        [HttpPut("update-user/{userId}")]
        [Authorize]
        public async Task<IActionResult> UpdateUser(Guid userId, UpdateUserDto updateUserDto)
        {
            var updatedTodoItem = await _userService.UpdateUserAsync(userId, updateUserDto);

            if (updatedTodoItem == null)
            {
                return NotFound($"User with id ({userId}) was not found.");
            }
            return CreatedAtAction(nameof(GetUser), new { id = userId }, updatedTodoItem);
        }
    }
}
