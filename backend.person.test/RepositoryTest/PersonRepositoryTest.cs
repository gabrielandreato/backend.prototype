using backend.person.datalibrary.DataContext;
using backend.person.datalibrary.Repository;
using backend.person.datalibrary.Repository.Interfaces;
using backend.person.modellibrary.DataModel;
using FluentAssertions;

namespace backend.person.test.RepositoryTest;

public class PersonRepositoryTest
{
    private readonly IPersonRepository _personRepository;

    public PersonRepositoryTest()
    {
        IPersonDataContext context = new TestDataContext();
        _personRepository = new PersonRepository(context);

        context.Person.Add(new Person
        {
            FirstName = "João",
            LastName = "Silva",
            Age = 32
        });
        context.SaveChanges();
    }

    [Fact]
    private void PersistTest()
    {
        //Arrange
        var person = new Person
        {
            Id = 0,
            FirstName = "João",
            LastName = "Mendes",
            Age = 31
        };

        //Act
        var result = _personRepository.Persist(person);
        
        //Assert
        result.Should().NotBeNull();
        result.Id.Should().BeGreaterThan(0);
    }

}