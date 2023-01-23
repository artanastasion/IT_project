using dbase.Models;
using dbase.converters;
using domain;
using domain.Data.Interfaces;
using domain.Data.Models;

namespace dbase.Repositories
{
    public class UserRepository: IUserRepository
    {
        private readonly ApplicationContext context;
    
        public UserRepository(ApplicationContext context)
        {
            context = context;
        }
        
        public User Create(User item)
        {
            return context.Add(item.ToDomain()).Entity.ToModel();
        }
    
        public Users? Delete(int id)
        {
            var item = GetItem(id);
            if (item == default)
                return null;
            return context.Remove(item.ToDomain()).Entity.ToModel();
        }

        public IEnumerable<Users> GetAll()
        {
            return context.Users.Select(item => item.ToDomain());
        }

        public Users? GetItem(int id)
        {
            return context.Users.FirstOrDefault(item => item.Id == id)?.ToDomain();
        }

        public Users? GetUserByLogin(string login)
        {
            return context.Users
                .FirstOrDefault(item => item.Name == login)?
                .ToDomain();
        }

        public void Save()
        {
            context.SaveChangesAsync();
        }

        public User Update(User item)
        {
            return context.Users.Update(item.ToModel()).Entity.ToDomain();
        }
    }
}

