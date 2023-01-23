using dbase.Models;
using dbase.converters;
using domain.Data.Interfaces;
using domain.Data.Models;
namespace dbase.Repositories;

public class ReciepentRepository
{
    private readonly ApplicationContext context;

    public ReciepentRepository(ApplicationContext context)
    {
        this.context = context;
    }

    public Reception Create(Reception item)
    {
        return context.Reception.Add(item.ToModel()).Entity.ToDomain();
    }

    public Reception? Delete(int id)
    {
        var item = GetItem(id);
        if (item != default)
            return context.Reception.Remove(item.ToModel()).Entity.ToDomain();
        return null;

    }

    public IEnumerable<Reception> GetAll()
    {
        return context.Reception.Select(item => item.ToDomain());
    }

    public IEnumerable<Reception> GetAppointments(int DoctorId)
    {
        return context.Reception
            .Where(item => item.DoctorId == DoctorId)
            .Select(item => item.ToDomain());
    }

    public IEnumerable<Reception> GetAppointments(Specialization specialization)
    {
        var doctors = context.Reception
            .Where(doctor => doctor.SpecializationId == specialization.Id)
            .Select(doctor => doctor.Id);
        return context.Reception
            .Where(item => doctors.Contains(item.DoctorId))
            .Select(item => item.ToDomain());
    }

    public Reception? GetItem(int id)
    {
        return context.Reception.FirstOrDefault(Reception => Reception.Id == id)?.ToDomain();
    }

    public void Save()
    {
        context.SaveChangesAsync();
    }

    public Reception Update(Reception item)
    {
        return context.Reception.Update(item.ToModel()).Entity.ToDomain();
    }
}