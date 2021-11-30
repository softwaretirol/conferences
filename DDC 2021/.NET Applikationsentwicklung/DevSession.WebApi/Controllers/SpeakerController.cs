using DevSession.EfCore;
using DevSession.EfCore.SqlServer;
using DevSession.WebApi.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace DevSession.WebApi.Controllers;

[Route("[controller]/[action]")]
public class SpeakerController : ControllerBase
{
    private readonly IDbContextFactory<DdcContextSqlServer> _dbFactory;
    private readonly IHubContext<DdcHub> _hub;

    public SpeakerController(IDbContextFactory<DdcContextSqlServer> dbFactory, IHubContext<DdcHub> hub)
    {
        _dbFactory = dbFactory;
        _hub = hub;
    }


    [HttpGet]
    public async Task<List<DdcSpeakerInfo>> GetAll() // /speaker/getall
    {
        await using var db = await _dbFactory.CreateDbContextAsync();

        return await db
            .Speakers
            .Select(x => new DdcSpeakerInfo
            {
                Id = x.Id,
                FirstName = x.FirstName
            })
            .ToListAsync();
    }

    [HttpPost]
    public async Task AddNewSpeaker()
    {
        await using var db = await _dbFactory.CreateDbContextAsync();
        await db.Speakers.AddAsync(new DdcSpeaker()
        {
            FirstName = DateTime.Now.ToShortDateString(),
            LastName = DateTime.Now.ToShortTimeString()
        });
        await db.SaveChangesAsync();
        await NewSpeakerAdded();
    }
    public async Task NewSpeakerAdded()
    {
        await _hub
            .Clients
            .All
            .SendAsync("SpeakerAdded");
    }
}