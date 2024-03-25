using TodoApi.Models;

public interface IUserService {
    Task<User?> CreateUserAsync(CreateUserDto createUserDto);
    Task<IEnumerable<User>> GetUsersAsync();
    Task<User?> GetUserAsync(Guid userId);
    Task<User?> UpdateUserAsync(Guid userId, UpdateUserDto updateUserDto);
    Task<AuthenticateResponseDto?> LoginUser(LoginUserDto loginUserDto);
}