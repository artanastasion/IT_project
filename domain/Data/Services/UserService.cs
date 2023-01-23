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

        var user = _repository.GetUserByLogin(login);

        return user is null ? Result.Fail<Users>("Пользователь не найден") : Result.Ok(user);
    }
    

    
    public Result<Users> CheckUser(Users user)
    {
        if (string.IsNullOrEmpty(user.Login))
            return Result.Fail<Users>("Укажите логин");

        var userCheck = _repository.CheckUser(user);
        return userCheck is null ? Result.Fail<Users>("Нет") : Result.Fail<Users>("да");

    }
    
    public Result<Users> Create(Users user)
    {
        if (string.IsNullOrEmpty(user.Login))
            return Result.Fail<Users>("Укажите логин");
        
        if (CheckUser(user) == Result.Fail<Users>("да"))
            return Result.Fail<Users>("Пользователь с таким лоогином уже существует");
            
        var userCreate = _repository.Create(user);
        return userCreate is null ? Result.Fail<Users>("Пользователь не может быть создан") : Result.Ok(userCreate);
            
    }
    public Result<Users> Update(Users user)
    {
        if (string.IsNullOrEmpty(user.Login))
            return Result.Fail<Users>("Укажите логин");
        
        if (CheckUser(user) == Result.Fail<Users>("да"))
            return Result.Fail<Users>("Пользователь с таким лоогином уже существует");
            
        var userCreate = _repository.Create(user);
        return userCreate is null ? Result.Fail<Users>("Пользователь не может быть изменен") : Result.Ok(userCreate);
            
    }
} 