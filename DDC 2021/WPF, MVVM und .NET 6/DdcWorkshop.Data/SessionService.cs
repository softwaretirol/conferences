using DdcWorkshop.Data.Contracts;

namespace DdcWorkshop.Data;

internal class SessionService : ISessionService
{
    public async Task<ICollection<Session>> GetAll()
    {
        await Task.Delay(4_000);
        var result = Enumerable.Range(0, 100_000).Select(x => new Session
        {
            Description = x.ToString(),
            Title = x.ToString(),
        }).ToList();

        return result;
    }

    public async IAsyncEnumerable<Session> GetAllAsStream()
    {
        while (true)
        {
            await Task.Delay(50);
            yield return new Session()
            {
                Description = DateTime.Now.ToString(),
                Title = DateTime.Now.ToString(),
            };
        }
    }
}