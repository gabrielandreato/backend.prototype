using backend.person.datalibrary.DataContext;
using backend.person.datalibrary.Repository.Interfaces;
using backend.person.modellibrary.DataModel;

namespace backend.person.datalibrary.Repository;

public class PersonRepository : IPersonRepository
{
    private readonly IPersonDataContext _context;

    public PersonRepository(IPersonDataContext context)
    {
        _context = context;
    }

    public async Task<Person> Persist(Person person)
    {
        await _context.Person.AddAsync(person);
        _context.SaveChanges();
        return person;
    }
}