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
            return Enumerable
                .Range(0, 100)
                .Skip(page * pageSize)
                .Take(pageSize)
                .Select(x => new Person
                {
                    Id = x,
                    Vorname = "Christian" + x,
                    Nachname = "Giesswein" + x
                }).ToArray();
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