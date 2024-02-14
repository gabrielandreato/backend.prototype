using backend.person.modellibrary.DataModel;
using backend.person.modellibrary.Utils;

namespace backend.person.datalibrary.Repository.Interfaces;

public interface IPersonRepository
{
    Person Persist(Person person);
    Person Remove(int id);
    Person GetByPk(int id);

    PagedList<Person> GetList(int[]? ids = null, string? firstName = null, string? lastName = null,
        int startAge = 0, int endAge = 0, int page = 0, int pageSize = 0);
}