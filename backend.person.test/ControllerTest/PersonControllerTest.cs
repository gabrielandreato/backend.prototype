using backend.person.api.Controller;
using backend.person.test.Mocks.PersonMocks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace backend.person.test.ControllerTest;

public class PersonControllerTest
{
    private readonly PersonController _personController;
    private readonly PersonMocks _personMocks = new();

    public PersonControllerTest()
    {
        _personController = new PersonController(_personMocks.PersonServiceMock.Object);
    }

    [Fact]
    private void PersistSuccessTest()
    {
        //Arrange
        _personMocks.CreatePersonPersistSuccessMock();

        //Act
        var response = _personController.Persist(_personMocks.CreatePersonDto);
        var okResult = response as OkObjectResult;

        //Assert
        okResult!.StatusCode.Should().Be(200);
    }

    [Fact]
    private void PersistExceptionTest()
    {
        //Arrange
        _personMocks.CreatePersonPersistExceptionMock();

        //Act
        var response = _personController.Persist(_personMocks.CreatePersonDto);
        var okObjectResponse = response as OkObjectResult;

        //Assert
        okObjectResponse?.StatusCode.Should().Be(400);
        okObjectResponse?.Value.Should().Be(PersonMocks.SimulatedException);
    }
}