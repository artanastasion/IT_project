namespace domain.Data.Models;

public class Doctor
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int Specialization { get; set; }
    
    public Doctor(int id, string name, int specializationId)
    {
        Id = id;
        Name = name;
        Specialization = specializationId;
    }

    public Doctor() : this(0, "default", 0) { }
    public Result IsValid()
    {
        if (Id < 0)
            return Result.Fail("Incorrect id");
        if (string.IsNullOrEmpty(Name))
            return Result.Fail("Incorrect doctor name");
        if (Specialization < 0)
            return Result.Fail("Incorrect specialization id");
        return Result.Ok();
    }
}