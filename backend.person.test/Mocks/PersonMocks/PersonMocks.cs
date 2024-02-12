using backend.person.api.Services.Interfaces;
using backend.person.datalibrary.Dto;
using backend.person.datalibrary.Repository.Interfaces;
using backend.person.modellibrary.DataModel;
using Moq;
using Xunit.Sdk;

namespace backend.person.test.Mocks.PersonMocks;

public class PersonMocks
{
    public readonly Mock<IPersonRepository> PersonRepositoryMock = new();
    public readonly Mock<IPersonService> PersonServiceMock = new();
    public const string? SimulatedException = "Simulated exception";

    public PersonMocks()
    {
        const string firstName = "João";
        const string lastName = "Silva";
        const int age = 22;
        Person = new Person { FirstName = firstName, LastName = lastName, Age = age };
        CreatePersonDto = new CreatePersonDto { FirstName = firstName, LastName = lastName, Age = age };
    }

    public CreatePersonDto CreatePersonDto { get; set; }
    public Person Person { get; set; }

    public void BuildMockPersonPersistSuccess()
    {
        PersonRepositoryMock.Setup(x =>
            x.Persist(It.Is<Person>(p =>
                p.FirstName == CreatePersonDto.FirstName
                && p.LastName == CreatePersonDto.LastName)
            )
        ).ReturnsAsync(() =>
        {
            Person.Id = 32;
            return Person;
        });

        PersonServiceMock.Setup(x =>
            x.PersistAsync(It.Is<CreatePersonDto>(p =>
                p.FirstName == CreatePersonDto.FirstName
                && p.LastName == CreatePersonDto.LastName)
            )
        ).ReturnsAsync(Person);
    }
    public void BuildMockPersonPersistException()
    {
        PersonRepositoryMock.Setup(x =>
            x.Persist(It.Is<Person>(p =>
                p.FirstName == CreatePersonDto.FirstName
                && p.LastName == CreatePersonDto.LastName)
            )
        ).ThrowsAsync(new Exception(SimulatedException));

        PersonServiceMock.Setup(x =>
            x.PersistAsync(It.Is<CreatePersonDto>(p =>
                p.FirstName == CreatePersonDto.FirstName
                && p.LastName == CreatePersonDto.LastName)
            )
        ).ThrowsAsync(new Exception(SimulatedException));
    }
}