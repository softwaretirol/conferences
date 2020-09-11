using System;
using System.Threading.Tasks;
using DDFWorkshop.UI.Contracts;
using Microsoft.AspNetCore.SignalR.Client;

namespace DDFWorkshop.WASM.Services
{
    public class ClientSideCounterService : ICounterService
    {
        private HubConnection hubConnection;
        private int lastReceivedCounter;

        public Task<int> ReadCurrentCounter()
        {
            return Task.FromResult(lastReceivedCounter);
        }

        public event Action CounterChanged;

        public async Task Increase()
        {
            await hubConnection.InvokeAsync("Increase");
        }

        public async Task Connect()
        {
            hubConnection = new HubConnectionBuilder()
                .WithUrl("https://localhost:44370/_CounterService")
                .Build();
            hubConnection.On<int>("CounterChanged", newCounterValue =>
            {
                lastReceivedCounter = newCounterValue;
                OnCounterChanged();
            });
            await hubConnection.StartAsync();
        }

        protected virtual void OnCounterChanged()
        {
            CounterChanged?.Invoke();
        }
    }
}