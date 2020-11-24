using System.Collections.ObjectModel;
using Microsoft.AspNetCore.SignalR.Client;
using Project.Client;

namespace E2E.WpfClient
{
    public class MainWindowViewModel
    {
        private readonly IConference2Client conferenceClient;
        private HubConnection hubConnection;

        public MainWindowViewModel(IConference2Client conferenceClient)
        {
            this.conferenceClient = conferenceClient;
            LoadAll();
        }

        public ObservableCollection<ConferenceModel> AllConferences { get; } = new();

        private async void LoadAll()
        {
            var result = await conferenceClient.GetAllAsync();
            foreach (var item in result)
            {
                AllConferences.Add(item);
            }

            hubConnection = new HubConnectionBuilder()
                .WithUrl("https://localhost:44320/DataChanged")
                .WithAutomaticReconnect()
                .Build();
            hubConnection.On<string, int>("Notify", (entity, id) =>
            {

            });
            await hubConnection.StartAsync();
        }
    }
}