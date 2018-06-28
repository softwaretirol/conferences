using System;
using System.Threading.Tasks;

namespace StandardLib
{
    public class ValueClient
    {
        public async Task<string[]> Get()
        {
            await Task.Delay(5000);
            return new string[] {"Hallo", "Welt"};

        }
    }
}
