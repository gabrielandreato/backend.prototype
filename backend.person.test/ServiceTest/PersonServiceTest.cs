using AutoMapper;
using backend.person.api.Services;
using backend.person.api.Services.Interfaces;
using backend.person.datalibrary.Profiles;
using backend.person.test.Mocks.PersonMocks;
using FluentAssertions;

namespace backend.person.test.ServiceTest;

public class PersonServiceTest
{
    private readonly PersonMocks _personMocks = new();
    private readonly IPersonService _personService;

    public PersonServiceTest()
    {
        _personMocks.BuildMockPersonPersistSuccess();
        var mapperConfig = new MapperConfiguration(cfg => { cfg.AddProfile<PersonProfile>(); });
        var mapper = mapperConfig.CreateMapper();
        _personService = new PersonService(_personMocks.PersonRepositoryMock.Object, mapper);
    }

    [Fact]
    private async void PersistAsyncTest()
    {
        //Arrange

        //Act
        var result = await _personService.PersistAsync(_personMocks.CreatePersonDto);

        //Assert
        result.Should().NotBeNull();
        result.Id.Should().BeGreaterThan(0);
    }
}