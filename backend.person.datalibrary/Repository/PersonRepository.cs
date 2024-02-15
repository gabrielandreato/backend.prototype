using backend.person.datalibrary.DataContext;
using backend.person.datalibrary.Repository.Interfaces;
using backend.person.modellibrary.DataModel;
using backend.person.modellibrary.Utils;
using Microsoft.EntityFrameworkCore;

namespace backend.person.datalibrary.Repository;

public class PersonRepository : IPersonRepository
{
    private readonly IPersonDataContext _context;

    public PersonRepository(IPersonDataContext context)
    {
        _context = context;
    }

    public Person Persist(Person person)
    {
        var queriedPerson =
            _context.Person
                .AsNoTracking()
                .FirstOrDefault(x => x.FirstName == person.FirstName && x.LastName == person.LastName);
        if (queriedPerson != null)
        {
            person.Id = queriedPerson.Id;
            _context.Person.Update(person);
        }
        else
            _context.Person.Add(person);

        _context.SaveChanges();
        return person;
    }

    public PagedList<Person> GetList(int[]? ids = null, string? firstName = null, string? lastName = null,
        int startAge = 0, int endAge = 0, int page = 0, int pageSize = 0)
    {
        var query =
            from person in _context.Person
            where
                (ids == null || ids.Length == 0 || ids.Contains(person.Id))
                && (firstName == null || firstName == person.FirstName)
                && (lastName == null || lastName == person.LastName)
                && (startAge == 0 || person.Age >= startAge)
                && (endAge == 0 || person.Age <= endAge)
            select person;
        
        return PagedList<Person>.Create(query, page, pageSize);
    }

    public Person Remove(int id)
    {
        var person = GetByPk(id);
        _context.Person.Remove(person);
        _context.SaveChanges();
        return person;
    }

    public Person GetByPk(int id)
    {
        try
        {
            return _context.Person.First(x => x.Id == id);
        } catch (Exception e)
        {
            throw new ApplicationException("Id cannot be found", e);
        }
    }
}