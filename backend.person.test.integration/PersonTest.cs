using backend.person.datalibrary.Dto;
using backend.person.modellibrary.DataModel;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace backend.person.test.integration;

public class PersonTest: IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    
    public PersonTest(CustomWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }

    [Fact]
    public async Task GetListPerson()
    {
        // Arrange

        // Act
        var response = await _client.GetAsync("/Person/List");

        // Assert
        response.EnsureSuccessStatusCode();
        response.Content.Headers.ContentType?.ToString().Should().Be("application/json; charset=utf-8");
    }
    
    [Fact]
    public async Task PostPerson()
    {
        // Arrange
        var person = new CreatePersonDto
        {
            FirstName = "Gabriel",
            LastName = "Andreato",
            Age = 27
        };

        var content = JsonContent.Create(person);
        // Act
        var response = await _client.PostAsync("/Person/Persist", content);

        // Assert
        response.EnsureSuccessStatusCode();
        response.Content.Headers.ContentType?.ToString().Should().Be("application/json; charset=utf-8");
        
        var result = await response.Content.ReadFromJsonAsync<Person>();
        result?.Id.Should().NotBe(0);
    }
}