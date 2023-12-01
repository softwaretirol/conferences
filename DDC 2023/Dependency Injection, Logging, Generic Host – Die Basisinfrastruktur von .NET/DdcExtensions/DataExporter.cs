using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace DdcExtensions;

public interface IDataExporter
{
    Task<string> ExportPersonsToJson();
}

internal class DataExporter : IDataExporter
{
    private readonly DataProvider _dataProvider;

    public DataExporter(DataProvider dataProvider, ILogger<DataExporter> logger)
    {
        _dataProvider = dataProvider;
        logger.LogInformation("DataExporter created {CreateDate}", DateTime.Now);
    }
    
    public async Task<string> ExportPersonsToJson()
    {
        return JsonSerializer.Serialize(await _dataProvider.GetPersons());
    }
}