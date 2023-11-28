using BlazorUI.Contracts;

internal class MyApiPersonDataSource : IPersonDataSource
{
    public Task<List<Person>> Get()
    {
        return Task.FromResult(new List<Person>());
    }
}