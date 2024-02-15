using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.person.modellibrary.DataModel;

public class Person
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public int Age { get; set; }

    public static Person Create(string firstName, string lastName, int age)
    {
        return new Person
        {
            FirstName = firstName,
            LastName = lastName,
            Age = age
        };
    }

}