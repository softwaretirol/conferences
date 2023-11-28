using BlazorUI.Contracts;

namespace WinFormsSample
{
    internal class MyLocalDbPersonDataSource : IPersonDataSource
    {
        public async Task<List<Person>> Get()
        {
            await Task.Delay(1000);
            return Enumerable.Range(0, 100)
                .Select(i => new Person(i.ToString(), i.ToString()))
                .ToList();
        }
    }
}