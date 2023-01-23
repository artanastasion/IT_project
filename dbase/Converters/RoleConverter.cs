
using RoleDB = dbase.Models.Role;
using RoleDomain = domain.Data.Models.Role;
namespace dbase.converters;

public static class RoleConverter
{
    public static RoleDB ToModel(this RoleDomain model)
    {
        return new RoleDB
        {
            Id = model.Id,
            Name = model.Name,
        };
    }

    public static RoleDomain ToDomain(this RoleDB model)
    {
        return new RoleDomain
        {
            Id = model.Id,
            Name = model.Name,
        };
    }
}