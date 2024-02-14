using backend.person.api.Services.Interfaces;
using backend.person.datalibrary.Dto;
using Microsoft.AspNetCore.Mvc;

namespace backend.person.api.Controller;

[ApiController]
[Route("[controller]")]
public class PersonController : ControllerBase
{
    private readonly IPersonService _personService;

    public PersonController(IPersonService personService)
    {
        _personService = personService;
    }

    /// <summary>
    /// Insert or Update a person, you cannot insert a person with the same name.
    /// </summary>
    /// <param name="person">Person to be Persisted</param>
    /// <returns>Persisted person</returns>
    [HttpPost("Persist")]
    public IActionResult Persist([FromBody] CreatePersonDto person)
    {
        try
        {
            var result = _personService.Persist(person);
            return Ok(result);
        } catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Get a list of Person
    /// </summary>
    /// <param name="ids">Unique identifier</param>
    /// <param name="firstName">First name for the person</param>
    /// <param name="lastName">First name for the person</param>
    /// <param name="startAge">The first age in the range</param>
    /// <param name="endAge">The last age in the range</param>
    /// <param name="page">The page to retrieve</param>
    /// <param name="pageSize">The number of objects to return</param>
    /// <returns>A list of Person</returns>
    [HttpGet("List")]
    public IActionResult GetList([FromQuery] string? ids, string? firstName, string? lastName, int startAge = 0,
        int endAge = 0,
        int page = 0, int pageSize = 0)
    {
        try
        {
            return Ok(_personService.GetList(ids, firstName, lastName, startAge, endAge, page, pageSize));
        } catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Get a person based in the unique identifier.
    /// </summary>
    /// <param name="id">Unique identifier.</param>
    /// <returns>A person</returns>
    [HttpGet("{id}")]
    public IActionResult GetByPk([FromRoute]int id)
    {
        try
        {
            return Ok(_personService.GetByPk(id));
        } catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Remove a person.
    /// </summary>
    /// <param name="id">Unique identifier</param>
    /// <returns>Removed person.</returns>
    [HttpDelete("{id}")]
    public IActionResult Remove([FromRoute] int id)
    {
        try
        {
            return Ok(_personService.Remove(id));
        } catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}