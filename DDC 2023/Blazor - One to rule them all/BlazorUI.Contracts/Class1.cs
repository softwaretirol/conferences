namespace BlazorUI.Contracts
{
    public interface IPersonDataSource
    {
        Task<List<Person>> Get();
    }

    public record Person(string FirstName, string LastName);
}
