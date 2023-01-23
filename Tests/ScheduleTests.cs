using System.Runtime.InteropServices.JavaScript;
using domain;
using domain.Data.Interfaces;
using domain.Data.Models;
using domain.Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace Tests;

public class ScheduleTests
{
    private readonly ScheduleService _scheduleService;
    private readonly Mock<IScheduleRepository> _sheduleRepositoryMock;

    public ScheduleTests()
    {
        _sheduleRepositoryMock = new Mock<IScheduleRepository>();
        _scheduleService = new ScheduleService(_sheduleRepositoryMock.Object);
    }

    [Fact]
    public void GetScheduleList_Ok()
    {
        _sheduleRepositoryMock.Setup(repository => repository.GetScheduleList()).Returns(() => new List<Schedule>());
        var res = _scheduleService.GetScheduleList();
        Assert.True(res.Success);  
    }
    
    [Fact]
    public void GetScheduleDoctor_FailNameDoctor()
    {
        var doctor = new Doctor(0, "", 0);
        DateTime time = DateTime.MinValue;
        var res = _scheduleService.GetScheduleDoctor(doctor, time);
        Assert.True(res.IsFailure); 
        Assert.Equal("Error: Incorrect doctor name", res.Error);
    }
    
    [Fact]
    public void GetScheduleDoctor_FailIdDoctor()
    {
        var doctor = new Doctor(-1, "as", 0);
        DateTime time = DateTime.MinValue;
        var res = _scheduleService.GetScheduleDoctor(doctor, time);
        Assert.True(res.IsFailure); 
        Assert.Equal("Error: Incorrect id", res.Error);
    }
    
    [Fact]
    public void GetScheduleDoctor_FailIdSpecialization()
    {
        var doctor = new Doctor(1, "as", -1);
        DateTime time = DateTime.MinValue;
        var res = _scheduleService.GetScheduleDoctor(doctor, time);
        Assert.True(res.IsFailure); 
        Assert.Equal("Error: Incorrect specialization id", res.Error);
    }

    [Fact]
    public void CreateScheduleDoctor_ScheduleFailTime()
    {
        DateTime start = new DateTime(2022, 12, 22);
        DateTime end = new DateTime(2022, 12, 23);
        var shedule = new Schedule(1, 0, start, end);
        var doctor = new Doctor(1, "as", 1);
        
        var res = _scheduleService.CreateScheduleDoctor(shedule);
        Assert.True(res.IsFailure); 
        Assert.Equal("Incorrect date", res.Error);
        
    }
    
    [Fact]
    public void CreateScheduleDoctor_ScheduleFailId()
    {
        var shedule = new Schedule(-1, 0, DateTime.MinValue, DateTime.MaxValue);
        var doctor = new Doctor(1, "as", 1);
        
        var res = _scheduleService.CreateScheduleDoctor(shedule);
        Assert.True(res.IsFailure); 
        Assert.Equal("Incorrect date", res.Error);
        
    }

}