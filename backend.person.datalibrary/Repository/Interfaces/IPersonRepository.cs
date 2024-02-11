using backend.person.modellibrary.DataModel;

namespace backend.person.datalibrary.Repository.Interfaces;

public interface IPersonRepository
{
    Task<Person> Persist(Person person);
}