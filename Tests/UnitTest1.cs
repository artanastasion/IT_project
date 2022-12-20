using domain.Data.Interfaces;
using domain.Data.Services;

namespace Tests;

public class UnitTest1
{
    private readonly UserService _userService;
    private readonly Mock<IUserRepository> _userRepositoryMock;

    public UnitTest1()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _userService = new UserService(_userRepositoryMock.Object);
    }

    
    [Fact]
    public void LoginIsEmptyOrNull_ShouldFail()
    {
        var res = _userService.GetUserByLogin(string.Empty);
        
        Assert.True(res.IsFailure); 
        Assert.Equal("Логин не был указан", res.Error); 
    }
    
    
    [Fact]
    public void UserNotFound_ShouldFail()
    {
        _userRepositoryMock.Setup(repository => repository.GetByLogin(It.IsAny<string>()))
            .Returns(() => null); 
        
        var res = _userService.GetUserByLogin("qwertyuiop"); 
        
        Assert.True(res.IsFailure); 
        Assert.Equal("Пользователь не найден", res.Error); 
    }
}