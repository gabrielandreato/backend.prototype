using backend.person.api.Services.Interfaces;
using backend.person.datalibrary.Dto;
using backend.person.datalibrary.Repository.Interfaces;
using backend.person.modellibrary.DataModel;
using backend.person.modellibrary.Utils;
using Moq;

namespace backend.person.test.Mocks.PersonMocks;

public class PersonMocks
{
    public const string? SimulatedException = "Simulated exception";
    public readonly Mock<IPersonRepository> PersonRepositoryMock = new();
    public readonly Mock<IPersonService> PersonServiceMock = new();

    public PersonMocks()
    {
        const string firstName = "Matheus";
        const string lastName = "Marques";
        const int age = 22;
        Person = new Person { FirstName = firstName, LastName = lastName, Age = age };
        CreatePersonDto = new CreatePersonDto { FirstName = firstName, LastName = lastName, Age = age };
    }

    public CreatePersonDto CreatePersonDto { get; set; }
    public Person Person { get; set; }

    public void CreatePersonPersistSuccessMock()
    {
        PersonRepositoryMock.Setup(x =>
            x.Persist(It.Is<Person>(p =>
                p.FirstName == CreatePersonDto.FirstName
                && p.LastName == CreatePersonDto.LastName)
            )
        ).Returns(() =>
        {
            Person.Id = 32;
            return Person;
        });

        PersonServiceMock.Setup(x =>
            x.Persist(It.Is<CreatePersonDto>(p =>
                p.FirstName == CreatePersonDto.FirstName
                && p.LastName == CreatePersonDto.LastName)
            )
        ).Returns(Person);
    }

    public void CreatePersonPersistExceptionMock()
    {
        PersonRepositoryMock.Setup(x =>
            x.Persist(It.Is<Person>(p =>
                p.FirstName == CreatePersonDto.FirstName
                && p.LastName == CreatePersonDto.LastName)
            )
        ).Throws(new Exception(SimulatedException));

        PersonServiceMock.Setup(x =>
            x.Persist(It.Is<CreatePersonDto>(p =>
                p.FirstName == CreatePersonDto.FirstName
                && p.LastName == CreatePersonDto.LastName)
            )
        ).Throws(new Exception(SimulatedException));
    }

    public void CreateRemoveTestMocks()
    {
        PersonRepositoryMock.Setup(s => s.Remove(It.Is<int>(id => id == Person.Id))
        ).Returns(Person);
        PersonServiceMock.Setup(s => s.Remove(It.Is<int>(id => id == Person.Id))
        ).Returns(Person);
    }

    public void CreateGetByPkTestMocks()
    {
        PersonRepositoryMock.Setup(s => s.GetByPk(It.Is<int>(id => id == Person.Id))
        ).Returns(Person);
        
        PersonServiceMock.Setup(s => s.GetByPk(It.Is<int>(id => id == Person.Id))
        ).Returns(Person);
    }

    public void CreateGetListMock()
    {
        var list = new List<Person>() { Person };
        var pagedList = new PagedList<Person>
        {
            Items = new List<Person>() { Person },
        };
        PersonRepositoryMock.Setup(s => s.GetList(
                It.IsAny<int[]?>(),
                It.Is<string>(id => id == Person.FirstName),
                It.Is<string>(id => id == Person.LastName),
                It.Is<int>(id => id == Person.Age),
                It.Is<int>(id => id == Person.Age),
                It.Is<int>(id => id == 1),
                It.Is<int>(id => id == 10)
            )
        ).Returns(pagedList);
        
        PersonServiceMock.Setup(s => s.GetList(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<int>()
            )
        ).Returns(pagedList);
    }
}