Console.WriteLine("Starting");
{
    var p = new Person()
    {
        FirstName = "John",
        LastName = "Doe"
    };
}

Console.WriteLine("Waiting");
Console.ReadLine();

public class Person
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public void SayHello()
    {
        
    }
}