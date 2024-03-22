using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TodoApi.Database;
using TodoApi.Models;
public class UserService : IUserService
{
    private readonly DatabaseContext _context;

    public UserService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<User?> CreateUserAsync(CreateUserDto createUserDto)
    {
        var existingUser = _context.Users.SingleOrDefault(u => u.UserName == createUserDto.UserName);
        if (existingUser != null)
        {
            return null;
        }

        var newUser = new User
        {
            UserId = Guid.NewGuid(),
            DisplayName = createUserDto.DisplayName,
            UserName = createUserDto.UserName,
            Role = UserRole.Guest,
            CreatedAt = DateTime.UtcNow,
            isDeleted = false,
        };

        byte[] hashedBytes = SHA256.HashData(Encoding.UTF8.GetBytes(createUserDto.Password));
        newUser.Password = Convert.ToBase64String(hashedBytes);

        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();

        return newUser;
    }
}