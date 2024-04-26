namespace DDF2024;

public class DataAccessLayer
{
    public List<string> GetNames()
    {
        return new List<string>
        {
            "1",
            "2",
            "3"
        };
    }

    public async Task Add(Person person)
    {
    }
}