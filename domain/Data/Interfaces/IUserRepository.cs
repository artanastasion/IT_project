using domain.Data.Models;

namespace domain.Data.Interfaces;

public interface IUserRepository : IRepository<Users>
{
    Users? GetUserByLogin(string login);
    Users CheckUser(Users item);
}