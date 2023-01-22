using domain.Data.Interfaces;
using domain.Data.Models;
using domain.Data.Services;

namespace Tests;

public class DoctorTests
{
    private readonly DoctorService _doctorService;
    private readonly Mock<IDoctorRepository> _doctorRepositoryMock;

    public DoctorTests()
    {
        _doctorRepositoryMock = new Mock<IDoctorRepository>();
        _doctorService = new DoctorService(_doctorRepositoryMock.Object);
    }
    
    [Fact]
    public void GetNameDoctor_ShouldFail()
    {
        var res = _doctorService.GetNameDoctor(string.Empty);
        
        Assert.True(res.IsFailure); 
        Assert.Equal("Incorrect doctor name", res.Error); 
    }

    [Fact]
    public void GetDoctorId_ShouldFail()
    {
        var res = _doctorService.GetDoctorId(-5);
        
        Assert.True(res.IsFailure); 
        Assert.Equal("Incorrect doctor id", res.Error); 
        
    }

    [Fact]
    public void Create_NullName()
    {
        
        Doctor doctor = new Doctor();
        doctor.Name = String.Empty;
        var res = _doctorService.Create(doctor);
        Assert.True(res.IsFailure); 
        Assert.Equal("Error: Incorrect doctor name", res.Error); 
        
    }
    
    [Fact]
    public void Create_NullSpecialization()
    {
        var doctor = new Doctor(1, "asd", -1);
        _doctorRepositoryMock.Setup(repository => repository.Create(It.IsAny<Doctor>())).Returns(() => doctor);
        var res = _doctorService.Create(doctor);
        Assert.True(res.IsFailure); 
        Assert.Equal("Error: Incorrect specialization id", res.Error); 
        
    }
    
    [Fact]
    public void Create_Null()
    {
        _doctorRepositoryMock.Setup(repository => repository.Create(It.IsAny<Doctor>())).Returns(() => new Doctor(-1, "", -1));
        var doctor = new Doctor(0, "asd", 0);
        var res = _doctorService.Create(doctor);
        Assert.True(res.IsFailure); 
        Assert.Equal("Doctor cannot be created", res.Error); 
        
    }
    [Fact]
    public void Delete_Null()
    {
        var doctor = new Doctor(0, "a", 1);
        _doctorRepositoryMock.Setup(repository => repository.GetDoctorId(It.IsAny<int>())).Returns(() => doctor);
        _doctorRepositoryMock.Setup(repository => repository.GetDoctorList()).Returns(() => new List<Doctor>() { new Doctor() });
        var result = _doctorService.Delete(0);

        Assert.True(result.IsFailure);
        Assert.Equal("Cannot delete doctor. Doctor has appointments", result.Error);
        
        
    }
    [Fact]
    public void GetDoctorList_Ok()
    {
        _doctorRepositoryMock.Setup(repository => repository.GetDoctorList()).Returns(() => new List<Doctor>());
        var res = _doctorService.GetDoctorList();
        Assert.True(res.Success);  
        
    }

    [Fact] public void GetSpecialisationDoctor_nullName()
    {
        Specialization specialization = new Specialization();
        specialization.Name = String.Empty;
        
        var res = _doctorService.GetSpecialisationDoctor(specialization);
        Assert.True(res.IsFailure); 
        Assert.Equal("Incorrect specialisation name", res.Error); 
        
    }
    
    [Fact] public void GetSpecialisationDoctor_null()
    {
        var spec = new Specialization();
        _doctorRepositoryMock.Setup(repository => repository.GetSpecialisationDoctor(spec)).Returns(() => new List<Doctor>());

        Specialization specialization = new Specialization();
        specialization.Name = String.Empty;
        
        var res = _doctorService.GetSpecialisationDoctor(specialization);
        Assert.True(res.IsFailure); 
        Assert.Equal("Incorrect specialisation name", res.Error); 
        
    }
}