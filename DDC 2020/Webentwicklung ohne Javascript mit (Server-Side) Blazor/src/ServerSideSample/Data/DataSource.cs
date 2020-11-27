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

        public IEnumerable<Person> GetAll(int skip, int take)
        {
            return allPersons
                .Skip(skip)
                .Take(take);
        }

        public void Remove( int personId)
        {
            allPersons.Remove(allPersons.FirstOrDefault(x => x.Id == personId));
        }
    }
}
