using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace DevSession.WebApi.Client
{
    public class DdcHubClient : IDisposable, IAsyncDisposable
    {
        private HubConnection _connection;

        public ValueTask DisposeAsync()
        {
            return _connection.DisposeAsync();
        }

        public void Dispose()
        {
            _connection.DisposeAsync();
        }

        public event Action OnSpeakerAdded;

        public async Task Start(string baseUrl)
        {
            _connection = new HubConnectionBuilder()
                .WithUrl(baseUrl)
                .WithAutomaticReconnect()
                .Build();
            _connection.On("SpeakerAdded", () => { OnSpeakerAdded?.Invoke(); });

            await _connection.StartAsync();
        }
    }
}