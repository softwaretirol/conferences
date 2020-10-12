using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace WDCWS.Data
{
    public class PersonServerSideQuery : IPersonQuery
    {
        public async Task<Person[]> GetAll(int page, int pageSize)
        {
            // DB
            await Task.Delay(2000);
            var watch = Stopwatch.StartNew();
            var persons = Enumerable
                .Range(0, 100_000)
                //.Skip(page * pageSize)
                //.Take(pageSize)
                .Select(x => new Person
                {
                    Id = x,
                    Vorname = "Christian" + x,
                    Nachname = "Giesswein" + x
                }).ToArray();
            watch.Stop();
            var w = watch.Elapsed.ToString();
            return persons;
        }

        public Person Store(Person person)
        {
            return person;
        }

        public async Task<Person> Get(int id)
        {
            await Task.Delay(1000);
            return new Person
            {
                Id = id,
                Vorname = "Christian" + id,
                Nachname = "Giesswein" + id
            };
        }
    }
}