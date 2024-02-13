using backend.person.modellibrary.DataModel;

namespace backend.person.datalibrary.Repository.Interfaces;

public interface IPersonRepository
{
    Person Persist(Person person);
    Person Delete(int id);
    Person GetByPk(int id);
}