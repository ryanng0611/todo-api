using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Database;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateNewUser(CreateUserDto createUserDto)
        {
            var createdNewUser = await _userService.CreateUserAsync(createUserDto);

            if (createdNewUser == null)
            {
                return NotFound("Something went wrong.");
            }

            return Ok(createdNewUser);
        }
    }
}
