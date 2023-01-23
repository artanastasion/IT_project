using domain.Data.Models;

namespace domain.Data.Interfaces;

public interface IUserRepository
{
    IEnumerable<Users> GetUserList();
    Users GetByLogin(string login);
    Users CheckUser(Users item);
    Users Create(Users item);
    Users Update(Users item);
    Users Delete(int id);
    Users Save();
}