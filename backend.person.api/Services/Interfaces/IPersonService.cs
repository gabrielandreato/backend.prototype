using backend.person.datalibrary.Dto;
using backend.person.modellibrary.DataModel;
using backend.person.modellibrary.Utils;

namespace backend.person.api.Services.Interfaces;

public interface IPersonService
{
    Person Persist(CreatePersonDto personDto);

    PagedList<Person> GetList(string? ids, string? firstName, string? lastName, int startAge, int endAge,
        int page, int pageSize);

    Person Remove(int id);

    Person GetByPk(int id);
}