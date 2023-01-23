using domain.Data.Interfaces;
using domain.Data.Models;
using domain.Data.Services;
namespace Tests;

public class ReceptionTests
{
    private readonly ReceptionService _receptionService;
    private readonly Mock<IReceptionRepository> _receptionRepositoryMock;

    public ReceptionTests()
    {
        _receptionRepositoryMock = new Mock<IReceptionRepository>();
        _receptionService = new ReceptionService(_receptionRepositoryMock.Object);
    }

    [Fact]
    public void SaveRecord_ReceptionFail()
    {
        var rep = new Reception(DateTime.MinValue, DateTime.MinValue, -2, -2);
        var sched = new Schedule();
        var res = _receptionService.SaveRecord(rep, sched);

        Assert.True(res.IsFailure);
        Assert.Equal("Invalid appointment: ", res.Error);
    }
    
    [Fact]
    public void SaveRecord_ReceptionInvalid()
    {
        var rec = new Reception(DateTime.MinValue, DateTime.MinValue, -2, -2);
        var sched = new Schedule();
        var res = _receptionService.SaveRecord(rec, sched);

        Assert.True(res.IsFailure);
        Assert.Equal("Invalid appointment: ", res.Error);
    }

    [Fact]
    public void Save_ScheduleFail()
    {
        var app = new Reception(DateTime.MinValue, DateTime.MinValue, 0, 0);
        var sched = new Schedule(-1, -2, DateTime.MinValue, DateTime.MinValue);
        var res = _receptionService.SaveRecord(app, sched);

        Assert.True(res.IsFailure);
        Assert.Equal("Invalid schedule: ", res.Error);
    }
    
    [Fact]
    public void Save_TimeFail()
    {
        var app = new Reception(DateTime.MaxValue, DateTime.MaxValue, 0, 0);
        var sched = new Schedule(0, 0, DateTime.MinValue, DateTime.MinValue);
        var res = _receptionService.SaveRecord(app, sched);

        Assert.True(res.IsFailure);
        Assert.Equal("Appointment out of schedule", res.Error);
    }
    
    [Fact]
    public void Save_SaveFailError()
    {
        List<Reception> apps = new();
        var app = new Reception();
        var sched = new Schedule(0,0, DateTime.MinValue, DateTime.MaxValue);
        var res = _receptionService.SaveRecord(app, sched);

        Assert.True(res.IsFailure);
        Assert.Equal("Unable to save appointment", res.Error);
    }

    [Fact]
    public void Save_Valid()
    {
        var app = new Reception();
        var sched = new Schedule(0,0, DateTime.MinValue, DateTime.MaxValue);
        var res = _receptionService.SaveRecord(app, sched);

        Assert.True(res.Success);
    }
    
    [Fact]
    public void UserId_Fail()
    {
        var app = new Reception(DateTime.MinValue, DateTime.MinValue, 0, -1);
        var check = app.IsValid();

        Assert.True(check.IsFailure);
        Assert.Equal("Incorrect patient ID", check.Error);
    }

    [Fact]
    public void DoctorID_Fail()
    {
        var app = new Reception(DateTime.MinValue, DateTime.MinValue, -1, 0);
        var check = app.IsValid();

        Assert.True(check.IsFailure);
        Assert.Equal("Incorrect doctor ID", check.Error);
    }
    
    [Fact]
    public void EmptyAppointment()
    {
        var app = new Reception();
        var check = app.IsValid();

        Assert.True(check.Success);
    }
}