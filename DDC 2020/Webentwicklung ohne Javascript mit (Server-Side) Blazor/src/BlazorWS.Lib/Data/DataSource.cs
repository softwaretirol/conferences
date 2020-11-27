using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerSideSample.Data
{
    public class DataSource
    {
        private readonly List<Person> allPersons = Enumerable
            .Range(0, 100)
            .Select(x => new Person
            {
                Id = x,
                Vorname = x.ToString(),
                Nachname = x.ToString(),
            })
            .ToList();

        public async Task<Person> Get(int id)
        {
            await Task.Delay(2000);
            return allPersons.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Person> GetAll(int skip, int take)
        {
            return allPersons
                .Skip(skip)
                .Take(take);
        }

        public event Action DataHasChanged;
        public void Remove(int personId)
        {
            allPersons.Remove(allPersons.FirstOrDefault(x => x.Id == personId));
            DataHasChanged?.Invoke();
        }

        public async Task Save(Person person)
        {
            await Task.Delay(2000);
            var personToChange = allPersons.FirstOrDefault(x => x.Id == person.Id);
            if (personToChange != null)
            {
                personToChange.Vorname = person.Vorname;
                personToChange.Nachname = person.Nachname;
                DataHasChanged?.Invoke();
            }
        }
    }
}
