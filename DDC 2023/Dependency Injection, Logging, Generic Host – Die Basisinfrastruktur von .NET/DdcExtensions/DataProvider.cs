using Microsoft.Extensions.DependencyInjection;

namespace DdcExtensions;

public class DataProvider
{
    private readonly DataProviderConfiguration _configuration;

    public DataProvider([FromKeyedServices("SpecialConfiguration2")]DataProviderConfiguration configuration)
    {
        _configuration = configuration;
    }
    public async Task<List<Person>> GetPersons()
    {
        await Task.Delay(1000);
        return Enumerable.Range(0, 100)
            .Select(x => new Person(x.ToString(), x.ToString()))
            .ToList();
    }
}

public class DataProviderConfiguration
{
    public DataProviderConfiguration()
    {
        
    }
}

public record Person(string FirstName, string LastName);