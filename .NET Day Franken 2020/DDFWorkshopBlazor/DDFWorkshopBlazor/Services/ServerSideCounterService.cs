using System;
using System.Threading.Tasks;
using DDFWorkshop.UI.Contracts;

namespace DDFWorkshopBlazor
{
    public class ServerSideCounterService : ICounterService
    {
        private int counter;

        public Task<int> ReadCurrentCounter()
        {
            return Task.FromResult(counter);
        }

        public event Action CounterChanged;

        public Task Increase()
        {
            counter++;
            CounterChanged?.Invoke();
            return Task.CompletedTask;
        }
    }
}