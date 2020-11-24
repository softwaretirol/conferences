using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E2E.EfCore;
using E2E.Queries.Models;
using Microsoft.EntityFrameworkCore;

namespace E2E.Queries
{
    public class ConferenceQueries //: IConferenceQueries
    {
        private readonly IDbContextFactory<E2EContext> factory;

        public ConferenceQueries(IDbContextFactory<E2EContext> factory)
        {
            this.factory = factory;
        }

        public async Task<IEnumerable<ConferenceModel>> QueryAll()
        {
            await using var db = factory.CreateDbContext();
            var allConferences = await db
                .Conferences
                .Select(x => new ConferenceModel(x.Id, x.Name, x.Date))
                //.ToList()
                //.Select(x => x with
                //{
                //    Date = DateTime.Now
                //})
                .ToListAsync();
            return allConferences;
        }
    }
}
