using backend.person.datalibrary.DataContext;
using backend.person.datalibrary.Repository;
using backend.person.datalibrary.Repository.Interfaces;
using backend.person.modellibrary.DataModel;
using backend.person.test.Mocks.PersonMocks;
using FluentAssertions;

namespace backend.person.test.RepositoryTest;

public class PersonRepositoryTest
{
    private readonly PersonMocks _personMocks = new();
    private readonly IPersonRepository _personRepository;
    private readonly IPersonDataContext _context;

    public PersonRepositoryTest()
    {
        IPersonDataContext context = new TestDataContext();
        _context = context;
        _personRepository = new PersonRepository(_context);
        _context.Person.Add(_personMocks.Person);
        _context.SaveChanges();
    }

    [Fact]
    private void PersistInsert()
    {
        //Arrange

        //Act
        var result = _personRepository.Persist(new Person
        {
            FirstName = "João",
            LastName = "Silva",
            Age = 23
        });

        //Assert
        result.Should().NotBeNull();
        result.Id.Should().BeGreaterThan(0);
    }

    [Fact]
    private void PersistUpdate()
    {
        //Arrange
        var person = Person.Create("Carlos", "Albuquerque", 30);
        _context.Person.Add(person);
        _context.SaveChanges();

        //Act
        var result = _personRepository.Persist(person);

        //Assert
        result.Should().NotBeNull();
        result.Id.Should().BeGreaterThan(0);
    }

    [Fact]
    private void GetList()
    {
        //Arrange
        var person = _personMocks.Person;

        //Act
        var result = _personRepository.GetList([person.Id], person.FirstName, person.LastName, person.Age, person.Age,
            1, 10);

        //Assert
        result.Items.Should().NotBeEmpty();
        result.Items.Select(x => x.Id).Should().Contain(person.Id);
    }

    [Fact]
    private void RemoveExistent()
    {
        //Arrange


        //Act
        var result = _personRepository.Remove(_personMocks.Person.Id);

        //Assert
        result.Id.Should().Be(_personMocks.Person.Id);
    }

    [Fact]
    private void RemoveInexistent()
    {
        //Arrange

        //Act
        var result = "";
        try
        {

            _personRepository.Remove(1000);
        } catch (Exception e)
        {
            result = e.Message;
        }

        //Assert
        result.Should().Be("Id cannot be found");
    }

    [Fact]
    private void GetByPk()
    {
        //Arrange

        //Act
        var result = _personRepository.GetByPk(_personMocks.Person.Id);

        //Assert
        result.Should().NotBeNull();
    }
}