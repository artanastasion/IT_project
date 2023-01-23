using dbase.Models;
using domain.Data.Models;
using UserDB = dbase.Models.User;
using UserDomain = domain.Data.Models.Users;

namespace dbase.converters;

public static class UserConverter
{
    public static UserDB ToModel(this UserDomain model)
    {
        return new UserDB
        {
            Id = model.Id,
            Name = model.Login,
            Password = model.Password,
            RoleId = model.RoleId,
            Phonenumber = model.PhoneNumber,
        };
    }
    
    public static UserDomain ToDomain(this UserDB model)
    {
        return new UserDomain
        {
            Id = model.Id,
            Login = model.Name,
            Password = model.Password,
            RoleId = model.RoleId,
            PhoneNumber = model.Phonenumber,
        };
    }
}