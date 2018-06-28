using System;
using System.Threading.Tasks;
using Orleans;
using Orleans.CodeGeneration;

namespace Interfaces
{
    public interface IChatRoom : IGrainWithStringKey
    {
        Task SendMessage(string message);
        Task<string[]> ResolveHistory();
    }

    [Version(1)]
    public interface IChatMessage : IGrainWithGuidKey
    {
        Task ForwardTo(string otherChatRoom);
        Task UpdateMessage(string message);
        Task<string> ResolveMessage();
    }
}
