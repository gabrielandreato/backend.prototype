﻿using backend.person.api.Controller;
using backend.person.modellibrary.DataModel;
using backend.person.test.Mocks.PersonMocks;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.person.test.ControllerTest;

public class PersonControllerTest
{
    private readonly PersonController _personController;
    private readonly PersonMocks _mocks = new();

    public PersonControllerTest()
    {
        _personController = new PersonController(_mocks.PersonServiceMock.Object);
    }

    [Fact]
    private void Persist200Test()
    {
        //Arrange
        _mocks.CreatePersonPersistSuccessMock();

        //Act
        var response = _personController.Persist(_mocks.CreatePersonDto);
        var okResult = response as OkObjectResult;

        //Assert
        okResult!.StatusCode.Should().Be(StatusCodes.Status200OK);
    }

    [Fact]
    private void Persist400Test()
    {
        //Arrange
        _mocks.CreatePersonPersistExceptionMock();

        //Act
        var response = _personController.Persist(_mocks.CreatePersonDto);
        var result = response as OkObjectResult;

        //Assert
        result?.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        result?.Value.Should().Be(PersonMocks.SimulatedException);
    }

    [Fact]
    private void GetByPk200Test()
    {
        //Arrange
        _mocks.CreateGetByPkTestMocks();
        //Act
        var response = _personController.GetByPk(_mocks.Person.Id);
        var result = response as OkObjectResult;
        var value = (Person)result!.Value!;

        //Assert
        result?.StatusCode.Should().Be(StatusCodes.Status200OK);
        value.Id.Should().Be(_mocks.Person.Id);
    }
    
    
    [Fact]
    private void GetList200Test()
    {
        //Arrange
        _mocks.CreateGetListMock();
        //Act
        var response = _personController.GetList();
        var result = response as OkObjectResult;

        //Assert
        result?.StatusCode.Should().Be(StatusCodes.Status200OK);
    }

    [Fact]
    private void Remove200Test()
    {
        //Arrange
        _mocks.CreateRemoveTestMocks();
        //Act
        var response = _personController.Remove(_mocks.Person.Id);
        var result = response as OkObjectResult;
        var value = (Person)result!.Value!;

        //Assert
        result.Should().NotBeNull();
        result?.StatusCode.Should().Be(StatusCodes.Status200OK);
        value.Id.Should().Be(_mocks.Person.Id);
        
    }


}
