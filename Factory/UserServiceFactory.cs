public interface IUserServiceFactory
{
    (IUserService UserService, IDisposable Scope) CreateUserService();
}

public class UserServiceFactory : IUserServiceFactory
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public UserServiceFactory(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public (IUserService UserService, IDisposable Scope) CreateUserService()
    {
        var scope = _serviceScopeFactory.CreateScope();
        var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
        return (userService, scope);
    }

}

