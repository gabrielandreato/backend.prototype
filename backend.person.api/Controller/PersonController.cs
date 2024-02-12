using backend.person.api.Services.Interfaces;
using backend.person.datalibrary.Dto;
using backend.person.modellibrary.DataModel;
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

    [HttpPost("Persist")]
    public async Task<IActionResult> Persist([FromBody] CreatePersonDto person)
    {
        try
        {
            var result = await _personService.PersistAsync(person);
            return Ok(result);
        } catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}