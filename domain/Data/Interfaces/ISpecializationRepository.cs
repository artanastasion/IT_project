using domain.Data.Models;

namespace domain.Data.Interfaces;

public interface ISpecializationRepository: IRepository<Specialization>
{
    Specialization? GetSpecializationList();
}