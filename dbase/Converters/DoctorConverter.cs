
using DoctorDB = dbase.Models.Doctor;
using DoctorDomain = domain.Data.Models.Doctor;
namespace dbase.converters;

public static class DoctorConverter
{
    public static DoctorDB ToModel(this DoctorDomain model)
    {
        return new DoctorDB
        {
            Id = model.Id,
            Name = model.Name,
            SpecializationId = model.Specialization,
        };
    }
    
    public static DoctorDomain ToDomain(this DoctorDB model)
    {
        return new DoctorDomain
        {
            Id = model.Id,
            Name = model.Name,
            Specialization = model.SpecializationId,
        };
    }
}