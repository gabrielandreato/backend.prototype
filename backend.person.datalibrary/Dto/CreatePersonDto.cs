namespace backend.person.datalibrary.Dto;

public class CreatePersonDto
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public int Age { get; set; }
}