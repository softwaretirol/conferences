using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E2E.Api.Hubs;
using E2E.EfCore;
using E2E.EfCore.Entities;
using E2E.Queries;
using E2E.Queries.Models;
using Microsoft.EntityFrameworkCore;

namespace E2E.Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class Conference2Controller : ControllerBase
    {
        private readonly ConferenceQueries queries;
        private readonly DataChangedHub hub;
        /* HTTP -
        Verb                    DbOperation (CRUD)
        GET     -   "Lesen" -   SELECT
        POST    -   "Anlegen" - INSERT
        DELETE  -   "Löschen" - DELETE
        PUT     -   "Ändern"  - UPDATE
        */

        public Conference2Controller(ConferenceQueries queries, DataChangedHub hub)
        {
            this.queries = queries;
            this.hub = hub;
        }

        [HttpGet]
        public async Task<IEnumerable<ConferenceModel>> GetAll()
        {
            return await queries.QueryAll();
        }

        [HttpPost]
        public async Task AddNewConference(AddNewConferenceModel model)
        {
            //await queries.Store(model);
            await hub.Save("Conference", 42);
        }
    }

    public record AddNewConferenceModel(string Title, DateTime Date);
}
