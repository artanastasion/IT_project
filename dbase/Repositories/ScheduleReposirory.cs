﻿using dbase.converters;
using domain.Data.Interfaces;
using domain.Data.Models;
namespace dbase.Repositories;

public class ScheduleReposirory: IScheduleRepository
{
    private readonly ApplicationContext context;

    public ScheduleRepository(ApplicationContext context)
    {
        this.context = context;
    }

    public Schedule Create(Schedule item, Doctor doctor)
    {
        return context.Add(item.ToModel()).Entity.ToDomain();
    }

    public Schedule Create(Schedule item)
    {
        return Create(item, new Doctor());
    }

    public Schedule? Delete(int id)
    {
        var schedule = GetItem(id);
        if (schedule == default)
            return null;
        return context.Remove(schedule.ToModel()).Entity.ToDomain();
    }

    public IEnumerable<Schedule> GetAll()
    {
        return context.Schedules.Select(item => item.ToDomain());
    }

    public Schedule? GetItem(int id)
    {
        return context.Schedules.FirstOrDefault(item => item.Id == id)?.ToDomain();
    }

    public Schedule? GetItem(Doctor doctor)
    {
        return context.Schedules.FirstOrDefault(item => item.DoctorId == doctor.Id)?.ToDomain();
    }

    public void Save()
    {
        context.SaveChangesAsync();
    }

    public Schedule? Update(Schedule item)
    {
        try
        {
            return context.Schedules.Update(item.ToModel()).Entity.ToDomain();
        }
        catch
        {
            return null;
        }
    }
}