using backend.person.datalibrary.DataContext;
using backend.person.datalibrary.Repository;
using backend.person.datalibrary.Repository.Interfaces;
using backend.person.test.Mocks.PersonMocks;
using FluentAssertions;

namespace backend.person.test.RepositoryTest;

public class PersonRepositoryTest
{
    private readonly PersonMocks _personMocks = new();
    private readonly IPersonRepository _personRepository;

    public PersonRepositoryTest()
    {
        IPersonDataContext context = new TestDataContext();
        _personRepository = new PersonRepository(context);

        context.Person.Add(_personMocks.Person);
        context.SaveChanges();
    }

    [Fact]
    private void PersistTest()
    {
        //Arrange

        //Act
        var result = _personRepository.Persist(_personMocks.Person);

        //Assert
        result.Should().NotBeNull();
        result.Id.Should().BeGreaterThan(0);
    }
}