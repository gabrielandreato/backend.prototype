using AutoMapper;
using backend.person.datalibrary.Dto;
using backend.person.modellibrary.DataModel;

namespace backend.person.datalibrary.Profiles;

public class PersonProfile: Profile
{
    public PersonProfile()
    {
        CreateMap<CreatePersonDto, Person>();
    }
}