using domain.Data.Interfaces;
using domain.Data.Models;

namespace domain.Data.Services;

public class UserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }
        
    public Result<Users> GetUserByLogin(string login)
    {
        if (string.IsNullOrEmpty(login))
            return Result.Fail<Users>("Логин не был указан");

        var user = _repository.GetByLogin(login);

        return user is null ? Result.Fail<Users>("Пользователь не найден") : Result.Ok(user);
    }
}