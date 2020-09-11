using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace DDFWorkshop.API.Hubs
{
    public class CounterServiceHub : Hub
    {
        private int counter;

        public async Task Increase()
        {
            counter++;
            await Clients.All.SendAsync("CounterChanged", counter);
        }
    }
}