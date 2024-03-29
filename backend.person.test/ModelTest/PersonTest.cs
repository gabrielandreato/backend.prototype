using System.Text.Json;
using AutoMapper;
using backend.person.datalibrary.Dto;
using backend.person.datalibrary.Profiles;
using backend.person.modellibrary.DataModel;
using backend.person.test.Mocks.PersonMocks;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;

namespace backend.person.test.ModelTest;

public class PersonTest
{
    private readonly PersonMocks _mocks = new();

    private string _jsonPerson =
        "{\"Id\":0,\"FirstName\":\"Matheus\",\"LastName\":\"Marques\",\"Age\":22}";

    private IMapper _mapper;

    public PersonTest()
    {
        var mapperConfig = new MapperConfiguration(cfg => { cfg.AddProfile<PersonProfile>(); });
        _mapper = mapperConfig.CreateMapper();
    }
    
    [Fact]
    private void Create()
    {
        //Arrange
        //Act
        var result = Person.Create(_mocks.Person.FirstName, _mocks.Person.LastName, _mocks.Person.Age);

        //Assert
        AssertProperties(result);
    }

    [Fact]
    private void Serialize()
    {
        //Arrange
        //Act
        var result = JsonSerializer.Serialize(_mocks.Person);

        //Assert
        result.Should().Be(_jsonPerson);
    }
    
    [Fact]
    private void Deserialize()
    {
        //Arrange
        //Act
        var result = JsonSerializer.Deserialize<Person>(_jsonPerson) 
                     ?? throw new TestCanceledException("Fail to deserialize Person");

        //Assert
        AssertProperties(result);
    }

    [Fact]
    private void MapToCreateDto()
    {
        //Arrange
        var createPersonDto = new CreatePersonDto
        {
            FirstName = _mocks.Person.FirstName,
            LastName = _mocks.Person.LastName,
            Age = _mocks.Person.Age
        };
            
        //Act
        var result = _mapper.Map<Person>(createPersonDto);

        //Assert
        result.Should().NotBeNull();
        AssertProperties(result);

    }

    private void AssertProperties(Person result)
    {

        result.FirstName.Should().Be(_mocks.Person.FirstName);
        result.LastName.Should().Be(_mocks.Person.LastName);
        result.Age.Should().Be(_mocks.Person.Age);
    }
}