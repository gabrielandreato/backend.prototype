using backend.person.api.Services.Interfaces;
using backend.person.datalibrary.Dto;
using backend.person.modellibrary.DataModel;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Swashbuckle.AspNetCore.Annotations;
using ILogger = Microsoft.Extensions.Logging.ILogger;

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
    /// /// /// <remarks>
    /// Sample request:
    ///
    ///     POST /Person/Persist
    ///     {
    ///        "firstName": "Gabriel",
    ///        "lastName": "Andreato",
    ///        "age": 27
    ///     }
    ///
    /// </remarks>
    [SwaggerResponse(StatusCodes.Status200OK, "Success to prosses the request.", typeof(Person))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Error to prosses the request.")]
    [HttpPost("Persist")]
    public IActionResult Persist([FromBody] CreatePersonDto person)
    {
        try
        {
            return Ok(_personService.Persist(person));
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
    [SwaggerResponse(StatusCodes.Status200OK, "Success to prosses the request.", typeof(List<Person>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Error to prosses the request.")]
    [HttpGet("List")]
    public IActionResult GetList([FromQuery] string? ids = null, string? firstName = null, string? lastName = null, int startAge = 0,
        int endAge = 0, int page = 0, int pageSize = 0)
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
    [SwaggerResponse(StatusCodes.Status200OK, "Success to prosses the request.", typeof(Person))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Error to prosses the request.")]
    [HttpGet("{id}")]
    public IActionResult GetByPk([FromRoute]int id)
    {
        try
        {
            var byPk = _personService.GetByPk(id);
            return Ok(byPk);
        } catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    
    
    /// <summary>
    /// Remove a person based in the unique identifier.
    /// </summary>
    /// <param name="id">Unique identifier.</param>
    [SwaggerResponse(StatusCodes.Status200OK, "Success to prosses the request.", typeof(Person))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Error to prosses the request.")]
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