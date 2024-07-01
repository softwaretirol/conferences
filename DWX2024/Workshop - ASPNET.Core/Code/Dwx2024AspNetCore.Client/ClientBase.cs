using System.Text.Json;

namespace Dwx2024AspNetCore.Client;

public abstract class ClientBase
{
    protected Task<HttpRequestMessage> CreateHttpRequestMessageAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(new HttpRequestMessage());
    }

    protected static void UpdateJsonSerializerSettings(JsonSerializerOptions settings)
    {
        settings.PropertyNameCaseInsensitive = true;
    }
}