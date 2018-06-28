using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Interfaces;
using Orleans;

namespace Implementations
{
    public class ChatRoom : Grain<List<Guid>>, IChatRoom
    {
        //private List<string> messages = new List<string>();
        public async Task SendMessage(string message)
        {
            Console.WriteLine(DateTime.Now);
            var messageId = Guid.NewGuid();
            var chatMessage = GrainFactory.GetGrain<IChatMessage>(messageId);
            await chatMessage.UpdateMessage(message);
            State.Add(messageId);
            await WriteStateAsync();
        }

        public async Task<string[]> ResolveHistory()
        {
            List<string> messages = new List<string>();
            foreach (var messageId in State)
            {
                var chatMessage = GrainFactory.GetGrain<IChatMessage>(messageId);
                string message = await chatMessage.ResolveMessage();
                messages.Add(message);
            }

            return messages.ToArray();
        }
    }
}
