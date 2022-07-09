using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DwxNet3
{
    public class MyPersonServiceConsumer
    {
        public MyPersonServiceConsumer(PersonService personService)
        {
            var person = personService.Get(42);

            Console.WriteLine(person.Vorname);
        }
    }
    public class Person
    {
        public string? Vorname { get; set; }
    }
    public class PersonService
    {

        public Person Get(int id)
        {
            return null!;
            //if(NotFound)
            if (id == 42)
            {
                throw new KeyNotFoundException();
            }
            return new Person();
        }
    }
}
