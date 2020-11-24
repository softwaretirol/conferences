using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace E2E.Api.Hubs
{
    public class DataChangedHub : Hub
    {
        public async Task Save(string entityName, int id)
        {
            await Clients.All.SendAsync("Notify", entityName, id);
        }
    }
}