using TodoApi.Models;

public interface IUserService {
    Task<User?> CreateUserAsync(CreateUserDto createUserDto);
    Task<IEnumerable<User>> GetUsersAsync();
    Task<User?> GetUserAsync(Guid userId);
    Task<User?> UpdateUserAsync(UpdateUserDto updateUserDto);
    Task<AuthenticateResponseDto?> LoginUser(LoginUserDto loginUserDto);
}