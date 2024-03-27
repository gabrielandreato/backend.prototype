using System;
using AutoMapper;
using backend.person.api.Services.Interfaces;
using backend.person.datalibrary.Dto;
using backend.person.datalibrary.Repository.Interfaces;
using backend.person.modellibrary.DataModel;
using backend.person.modellibrary.Utils;

namespace backend.person.api.Services;

public class PersonService : IPersonService
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;

    public PersonService(IPersonRepository personRepository, IMapper mapper)
    {
        _personRepository = personRepository;
        _mapper = mapper;
    }

    public Person Persist(CreatePersonDto personDto)
    {
        var person = _mapper.Map<Person>(personDto);
        return _personRepository.Persist(person);
    }

    public PagedList<Person> GetList(string? ids, string? firstName, string? lastName, int startAge, int endAge,
        int page, int pageSize)
    {
        var splittedIds = Array.ConvertAll(ids?.Split(",") ?? Array.Empty<string>(), int.Parse);
        return _personRepository.GetList(splittedIds, firstName, lastName, startAge, endAge, page, pageSize);
    }

    public Person Remove(int id)
    {
        return _personRepository.Remove(id);
    }

    public Person GetByPk(int id)
    {
        return _personRepository.GetByPk(id);
    }
}