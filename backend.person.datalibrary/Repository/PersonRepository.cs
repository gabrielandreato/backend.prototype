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

    public List<Person> GetList(List<int> ids, string firstName, string lastName, int startAge, int endAge)
    {
        var response = new PagedList<Person>();
         
    }

    public Person Delete(int id)
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