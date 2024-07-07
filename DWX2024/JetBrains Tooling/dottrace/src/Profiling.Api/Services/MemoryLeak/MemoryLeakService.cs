using System.Collections.Concurrent;
using System.Text;

namespace Profiling.Api.Services.MemoryLeak
{
    public class MemoryLeakService: IMemoryLeakService
    {

        private ConcurrentDictionary<Guid, object> _cache { get; } = new();

        private string _alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        private int messageBytesSize = 32000;

        public void Run()
        {
            AddToCache();
        }

        private void AddToCache()
        {
            _cache.GetOrAdd(Guid.NewGuid(), GetObject());
        }

        private object GetObject()
        {
            int charsAvailable = _alphabet.Length;

            var sb = new StringBuilder();
            for (int i = 0; i < messageBytesSize - 4; i++)
            {
                sb.Append(_alphabet[new Random().Next(charsAvailable - 1)]);
            }

            return sb.ToString();
        }
    }
}
