
// C# 7.3 <- .NET Framework

using CSharpSamples;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Runtime.Serialization.Formatters.Binary;


var numbers2 = new int[] { 1, 2, 3, 4 };
var numbers3 = new List<int>() { 1, 2, 3, 4 };
int[] numbers4 = [1, 2, 3, 4];
List<int> numbers5 = [1, 2, 3, 4, 5];

// ASP.NET Core Minimal APIs
//app.Get("/Person", (int? number = null) =>
//{
//  
//});
var a = () =>
{

};


string s = "Hallo\\asdasd";
string s1 = @"Hallo\asdasd";
string s2 = $@"{DateTime.Now:d} - {DateTime.Now:t}";
string s3 = $"""""""""
            
            SELECT * FROM 
            PERSONS
            WHERE
            FirstName = 'asdasd' and LastName = '""""'

            """"""""";
string s4 = """
            {
                Data: "Juhu",
                Number: 123,
                asdasd: {
                    Number: 1
                }
            }
            """;



List<int> ints = new();
var ints2 = new List<int>();

await using var stream = new MemoryStream();

// async / await
await foreach(var number in MySampleClass.GetNumbers())
{
    Console.WriteLine(number);
}

string s = null;
s ??= "Hallo";

object o = s;


Console.ReadLine();
Console.WriteLine("Hello, World!");

var person2 = new Person(1234);
var person3 = new Person()
{
    Age = 1234,
};
var person4 = new Person()
{
    Age = 123,
    Address = new()
    {

    }
};

//person2.Age = 123;

var person = new PersonRecord("Christian", "Giesswein");
Console.WriteLine(person.FirstName);

var personChanged1 = person with
{
    FirstName = "Hallo"
};
var personChanged2 = person with
{
    FirstName = "Hallo"
};

Console.WriteLine(personChanged1 == personChanged2);
//BinaryFormatter p = new();
MyOtherSampleClass p = new();
//person.FirstName = "Hallo";

record PersonRecord(string FirstName, string LastName);


//[Obsolete("Typ ist nicht mehr in Verwendung", true)]
//[Experimental("123")]
public class MyOtherSampleClass
{
    public void Juhu<T>()
        where T : IPerson
    {
        var instance = T.Create();

    }
}


public class NumberSpinner<T>
    where T : INumber<T>
{
    private T number;
    public void Increment()
    {
        number++;
    }
}



public class PrimaryConstructorWithCSharp12(string firstName, string lastName)
{
    public PrimaryConstructorWithCSharp12(string other) : this("Hallo", "123")
    {
    }

    public void Print()
    {
        firstName = "";
        Console.WriteLine(firstName);
    }
}

public class MyPerson : IPerson
{
    public static Guid Id { get; } = new Guid("A82CBF71-4389-4733-B373-1D659460DC77");
    public string Name => throw new NotImplementedException();

    public static IPerson Create()
    {
        return new MyPerson();
    }

    public static void SayHello()
    {
        throw new NotImplementedException();
    }
}
public interface IPerson
{
    string Name { get; }

    static abstract void SayHello();
    static abstract IPerson Create();
    static abstract Guid Id { get; }

    public void Print()
    {
        Console.WriteLine(Name);
    }
}

public class Person
{
    public required int Age { get; init; }
    public Address Address { get; set; }

    [SetsRequiredMembers]
    public Person(int age)
    {
        Age = age;
    }

    public Person()
    {
        
    }
}
public class Address
{
    public string Street { get; set; }
}
public class MySampleClass
{
    private static string config;
    public static void FetchData()
    {
        config ??= FetchConfig();
    }

    private static string FetchConfig()
    {
        return "";
    }

    public static async IAsyncEnumerable<int> GetNumbers()
    {
        await Task.Delay(1000);
        yield return 1;
        
        await Task.Delay(1000);
        yield return 2;

        await Task.Delay(1000);
        yield return 3;
    }
}