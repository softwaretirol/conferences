using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace HelloBlazor.Pages
{
    public class IndexController : BlazorComponent
    {
        [Inject]
        public HttpClient Http { get; set; }
        public Person[] Persons { get; set; }
        public int Number { get; set; } = 10;
        public async Task IncrementNumber()
        {
            Persons = await Http.GetJsonAsync<Person[]>("data.json");
            Number++;

        }

        public class Person
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }

        }
    }
}
