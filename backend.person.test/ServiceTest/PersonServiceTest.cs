using AutoMapper;
using backend.person.api.Services;
using backend.person.api.Services.Interfaces;
using backend.person.datalibrary.Dto;
using backend.person.datalibrary.Profiles;
using backend.person.datalibrary.Repository.Interfaces;
using backend.person.modellibrary.DataModel;
using FluentAssertions;
using Moq;

namespace backend.person.test.ServiceTest;

public class PersonServiceTest
{
    private readonly IPersonService _personService;
    private readonly Mock<IPersonRepository> _personRepositoryMock = new();

    public PersonServiceTest()
    {
        var mapperConfig = new MapperConfiguration(cfg => { cfg.AddProfile<PersonProfile>(); });
        var mapper = mapperConfig.CreateMapper();
        _personService = new PersonService(_personRepositoryMock.Object, mapper);
    }

    [Fact]
    private async void PersistAsyncTest()
    {
        //Arrange
        var createPersonDto = BuildMockPersonPersist();

        //Act
        var result = await _personService.PersistAsync(createPersonDto);
        
        //Assert
        result.Should().NotBeNull();
        result.Id.Should().BeGreaterThan(0);
    }

    private CreatePersonDto BuildMockPersonPersist()
    {

        CreatePersonDto createPersonDto = new()
        {
            FirstName = "João",
            LastName = "Silva",
            Age = 22
        };

        _personRepositoryMock.Setup(x =>
            x.Persist(It.Is<Person>(p => 
                p.FirstName == createPersonDto.FirstName
                && p.LastName == createPersonDto.LastName)
            )
        ).ReturnsAsync(new Person
        {
            Id = 32,
            FirstName = createPersonDto.FirstName,
            LastName = createPersonDto.LastName,
            Age = createPersonDto.Age
        });
        return createPersonDto;
    }
}