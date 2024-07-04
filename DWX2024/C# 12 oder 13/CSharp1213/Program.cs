using System.Text.RegularExpressions;
using person = (string firstname,string lastname);

Console.WriteLine("C# ist super");

var builder = WebApplication.CreateBuilder();
var app = builder.Build();
// Minimal APIs
app.MapGet("/{id}", (int? id = null) =>
{

});
app.Run();
person p3 = ("C", "G");
var a = (int x) => { return 42; };

List<int> numbers = [1, 2, 3, 4];
string.Join(",", ["A", "B", "C"]);

new Person("A", "B");

public class Person(string firstName, string lastName)
{
    public string FirstName => firstName;

    public void WriteToConsole()
    {
        Console.WriteLine(firstName + " " + lastName);
    }
}

public partial class MyHelper
{
    [GeneratedRegex("d+")]
    public partial Regex MyRegex();
}