using Ddc2022Blazor.HelloWorld.Models;

namespace Ddc2022Blazor.Rcl.Services;

public class PersonService
{
    public List<Person> Persons { get; } = new()
    {
        new Person
        {
            Name = "Hello DDC 2022",
            CreationDate = DateTime.Now
        }
    };
}