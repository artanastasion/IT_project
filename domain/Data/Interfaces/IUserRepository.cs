using domain.Data.Models;

namespace domain.Data.Interfaces;

public interface IUserRepository
{
    IEnumerable<Users> GetUserList();
    Users GetByLogin(string login);
    Users GetUser(int id);
    void Create(Users item);
    void Update(Users item);
    void Delete(int id);    
    void Save();
}