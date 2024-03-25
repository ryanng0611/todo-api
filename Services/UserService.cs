using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TodoApi.Database;
using TodoApi.Models;
public class UserService : IUserService
{
    private readonly AppSettings _appSettings;

    private readonly DatabaseContext _context;

    public UserService(IOptions<AppSettings> appSettings, DatabaseContext context)
    {
        _appSettings = appSettings.Value;
        _context = context;
    }

    public async Task<AuthenticateResponseDto?> LoginUser(LoginUserDto loginUserDto)
    {
            var user = await _context.Users.SingleOrDefaultAsync(
                x => x.UserName == loginUserDto.UserName && 
                x.Password == loginUserDto.Password);

            // return null if user not found
            if (user == null) return null;

            // authentication successful so generate jwt token
            var token = await generateJwtToken(user);

            return new AuthenticateResponseDto(user, token);
    }

    public async Task<User?> CreateUserAsync(CreateUserDto createUserDto)
    {
        var existingUser = _context.Users.SingleOrDefault(u => u.UserName
                                                               == createUserDto.UserName);
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
            EditedAt = DateTime.UtcNow,
            isDeleted = false,
        };

        byte[] hashedBytes = SHA256.HashData(Encoding.UTF8.GetBytes(createUserDto.Password));
        newUser.Password = Convert.ToBase64String(hashedBytes);

        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();

        return newUser;
    }

    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User?> GetUserAsync(Guid userId)
    {
        return await _context.Users.FindAsync(userId);
    }

    public async Task<User?> UpdateUserAsync(Guid userId, UpdateUserDto updateUserDto)
    {
        var userToUpdate = await _context.Users.FindAsync(userId);
        if (userToUpdate == null)
        {
            return null;
        }

        userToUpdate.DisplayName = updateUserDto.DisplayName ?? userToUpdate.DisplayName;
        userToUpdate.UserName = updateUserDto.UserName ?? userToUpdate.UserName;
        userToUpdate.Password = updateUserDto.Password ?? userToUpdate.Password;
        userToUpdate.EditedAt = DateTime.UtcNow;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!UserExists(userId))
            {
                return null;
            }
            else
            {
                throw;
            }
        }

        return userToUpdate;
    }

    private bool UserExists(Guid id)
    {
        return _context.Users.Any(e => e.UserId == id);
    }

    private async Task<string> generateJwtToken(User user)
    {
        //Generate token that is valid for 7 days
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = await Task.Run(() =>
        {
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity([new Claim("id", user.UserId.ToString())]),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            return tokenHandler.CreateToken(tokenDescriptor);
        });

        return tokenHandler.WriteToken(token);
    }
}