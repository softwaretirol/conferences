using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Messages
{
    public class StatusMessage
    {
        public StatusMessage(string message)
        {
            Message = message;
        }

        public DateTime SendTime { get; } = DateTime.Now;
        public string Message { get; }
    }
}
