using TodoApi.Models;

public interface IUserService {
    Task<User?> CreateUserAsync(CreateUserDto createUserDto);
}