using backend.person.api;
using backend.person.datalibrary.DataContext;
using backend.person.datalibrary.Dto;
using backend.person.modellibrary.DataModel;
using backend.person.modellibrary.Utils;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace backend.person.test.integration.Controllers;

public class PersonTest : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly IPersonDataContext _context;

    public PersonTest(CustomWebApplicationFactory<Program> factory)
    {
        _context = new TestDataContext();
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }

    [Fact]
    public async Task GetList()
    {
        // Arrange
        var person = _context.Person.Add(new Person
        {
            FirstName = "FirstName",
            LastName = "LastName",
            Age = 21
        });
        _context.SaveChanges();

        // Act
        var response = await _client.GetAsync("/Person/List");
        var result = await response.Content.ReadFromJsonAsync<PagedList<Person>>();

        // Assert
        response.EnsureSuccessStatusCode();
        result?.Items.First(p => p.Id == person.Entity.Id).FirstName.Should().Be("FirstName");
    }


    [Fact]
    public async Task Create()
    {
        // Arrange
        var firstName = "CreatedFirstName";
        var person = new CreatePersonDto
        {
            FirstName = firstName,
            LastName = "Andreato",
            Age = 27
        };

        var content = JsonContent.Create(person);
        // Act
        var response = await _client.PostAsync("/Person/Persist", content);
        var result = await response.Content.ReadFromJsonAsync<Person>();

        // Assert
        response.EnsureSuccessStatusCode();
        result?.Id.Should().NotBe(0);
    }

    [Fact]
    public async Task Delete()
    {
        // Arrange
        var firstName = "DeletedPerson";
        var person = _context.Person.Add(new Person
        {
            FirstName = firstName,
            LastName = "LastName",
            Age = 21
        });
        _context.SaveChanges();

        // Act
        var response = await _client.DeleteAsync($"/Person/{person.Entity.Id}");
        var result = await response.Content.ReadFromJsonAsync<Person>();

        // Assert
        response.EnsureSuccessStatusCode();
        result?.FirstName.Should().Be(firstName);
    }

    [Fact]
    public async Task GetByPk()
    {
        // Arrange
        var firstName = "GetByPkPerson";
        var person = _context.Person.Add(new Person
        {
            FirstName = firstName,
            LastName = "LastName",
            Age = 21
        });
        _context.SaveChanges();

        // Act
        var response = await _client.GetAsync($"/Person/{person.Entity.Id}");
        var result = await response.Content.ReadFromJsonAsync<Person>();

        // Assert
        response.EnsureSuccessStatusCode();
        result?.FirstName.Should().Be(firstName);
    }
}