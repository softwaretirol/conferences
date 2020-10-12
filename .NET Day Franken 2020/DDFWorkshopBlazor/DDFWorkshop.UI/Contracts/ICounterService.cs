using System;
using System.Threading.Tasks;

namespace DDFWorkshop.UI.Contracts
{
    public interface ICounterService
    {
        Task<int> ReadCurrentCounter();
        event Action CounterChanged;
        Task Increase();
    }
}