using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDFWorkshopBlazor
{
    public class CounterService
    {
        public int Counter { get; private set; }
        public event Action CounterChanged;
        public void Increase()
        {
            Counter++;
            CounterChanged?.Invoke();
        }
    }
}
