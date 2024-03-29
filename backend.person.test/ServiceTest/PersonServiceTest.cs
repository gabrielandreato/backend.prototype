using AutoMapper;
using backend.person.api.Services;
using backend.person.api.Services.Interfaces;
using backend.person.datalibrary.Profiles;
using backend.person.test.Mocks.PersonMocks;
using FluentAssertions;

namespace backend.person.test.ServiceTest;

public class PersonServiceTest
{
    private readonly PersonMocks _mocks = new();
    private readonly IPersonService _personService;

    public PersonServiceTest()
    {
        var mapperConfig = new MapperConfiguration(cfg => { cfg.AddProfile<PersonProfile>(); });
        var mapper = mapperConfig.CreateMapper();
        _personService = new PersonService(_mocks.PersonRepositoryMock.Object, mapper);
    }

    [Fact]
    private void Persist()
    {
        //Arrange
        _mocks.CreatePersonPersistSuccessMock();
        //Act
        var result = _personService.Persist(_mocks.CreatePersonDto);

        //Assert
        result.Should().NotBeNull();
        result.Id.Should().BeGreaterThan(0);
    }

    [Fact]
    private void Remove()
    {
        //Arrange
        _mocks.CreateRemoveTestMocks();

        //Act
        var result = _personService.Remove(_mocks.Person.Id);

        //Assert
        result.Id.Should().Be(_mocks.Person.Id);
    }

    [Fact]
    private void GetByPk()
    {
        //Arrange
        _mocks.CreateGetByPkTestMocks();
        _mocks.Person.Id = 25;

        //Act
        var result = _personService.GetByPk(_mocks.Person.Id);

        //Assert
        result.Id.Should().Be(_mocks.Person.Id);
    }

    [Fact]
    private void GetList()
    {
        //Arrange
        var person = _mocks.Person;
        _mocks.CreateGetListMock();
        //Act
        var result = _personService.GetList($"{person.Id}", person.FirstName, person.LastName, person.Age, person.Age,
            1, 10);

        //Assert
        result.Should().NotBeNull();
    }
}