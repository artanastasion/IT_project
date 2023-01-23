using ReceptionDB = dbase.Models.Reception;
using ReceptionDomain = domain.Data.Models.Reception;
namespace dbase.converters;

public static class ReceptionConverter
{
    public static ReceptionDB ToModel(this ReceptionDomain model)
    {
        return new ReceptionDB
        {
            Id = model.Id,
            StartReception = model.StartReception,
            EndReception = model.EndReception,
            DoctorId = model.DoctorId,
            UserId = model.UserId,
        };
    }
    
    public static ReceptionDomain ToDomain(this ReceptionDB model)
    {
        return new ReceptionDomain
        {
            Id = model.Id,
            StartReception = model.StartReception,
            EndReception = model.EndReception,
            DoctorId = model.DoctorId,
            UserId = model.UserId,
        };
    }
}