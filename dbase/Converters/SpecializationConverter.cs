using SpecializationDB = dbase.Models.Specialization;
using SpecializationDomain = domain.Data.Models.Specialization;
namespace dbase.converters;

public static class SpecializationConverter
{
    public static SpecializationDB ToModel(this SpecializationDomain model)
    {
        return new SpecializationDB
        {
            Id = model.Id,
            Name = model.Name,
        };
    }
    public static SpecializationDomain ToDomain(this SpecializationDB model)
    {
        return new SpecializationDomain
        {
            Id = model.Id,
            Name = model.Name,
        };
    }
}