using System.Threading.Tasks;
using System.Windows;
using AspNetCore.Client;
using DevSession.WebApi.Client;

namespace DevSession.WpfClient;

public partial class MainWindow : Window
{
    private DdcHubClient _hubClient;

    public MainWindow()
    {
        InitializeComponent();
        LoadAllSpeakers();
    }

    private async void LoadAllSpeakers()
    {
        await Refresh();

        _hubClient = new DdcHubClient();
        _hubClient.OnSpeakerAdded += OnSpeakerAdded;
        await _hubClient.Start("https://localhost:44340/ddchub");
    }

    private async Task Refresh()
    {
        ISpeakerClient client = new SpeakerClient("https://localhost:44340/", new());
        var speakers = await client.GetAllAsync();
        dg.ItemsSource = speakers;
    }

    private async void OnSpeakerAdded()
    {
        await Refresh();
    }
}