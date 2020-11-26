
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

var instance = new A.B();
System.Console.WriteLine("Hello World!");

Person p1 = new ("Christian", "Giesswein", new ("X", "Y"));
Person p2 = new ("Christian", "Giesswein", new ("X", "Y"));

bool isTrue = object.Equals(p1, p2) ||
              p1 == p2 ||
              p1.Equals(p2);
//Console.WriteLine(isTrue);

B b1 = new ();
B b2 = new ("A");
B b3 = new ("A1");

//Console.WriteLine(b1 == b2);
//Console.WriteLine(b2 == b3);

         // new B(b3) .Name="xyz"
var b4 = b3 with     { Name = "xyz" };


Dictionary<int, Person> persons = new()
{
    {1, new("C", "G", new("X", "Y"))}
};

int number = 42;
object o = new Person6();

if (o is Person6 { Vorname: { Length: >10 } } person)
{
}

string text = "Hallo Welt";
if (string.IsNullOrEmpty(text))
{

}


var letters = text.Where(static (x, _) => x > 'a').ToArray();

unsafe
{
    fixed (char* ptr = text)
    {
    }
}

nint n = 10;

if (text is { Length: > 0 } notNullText)
{
    //notNullText
}

if (text is not null and { Length: >0})
{

}


//if (number is > 10 and < 100 or 50)
//{
//}

//if (number > 10)
//{
//}
new Person2("", "");


// C# 9.0
// - record (Datenklasse)
// - init (Property kann in Initalisierungsphase gesetzt werden)
// - with (nur in Zusammenhang mit Records)

//var instance = new XYZ
//{
//    Name = "asd",
//};
//var test = instance with { Name = "asdasd"};
//public class XYZ
//{
//    public string Name { get; init; }
//}

public record B
{
    public string Name { get; init; }

    public B()
    {
        
    }
    public B(string name)
    {
        Name = name;
    }
}

public record A2 : B, IDisposable
{
    public void Dispose()
    {
    }
}



public class MyBase
{
    protected virtual B DoIt()
    {
        return new B();
    }
}

public class ChildClass : MyBase
{
    protected override A2 DoIt()
    {
        return new A2();
    }

    public void Juhu()
    {
        var a = DoIt();
    }
}







// .NET 5
// EF Core 5
// ASP.NET Core 5
// [HttpPost]
// public void Post(Person6 person)
// {
// }
// JsonSerializer //System.Text.Json


// record - class
public record Person6
{
    public string Vorname { get; init; }
    public string Nachname { get; init; }
    public string Person3 { get; init; }
}

public record Person(string Vorname, string Nachname, Person3 numbers);

public record Person3(string Vorname, string Nachname)
{
    private List<int> numbers = new();

    public void Juhu()
    {

    }
}

public class Person1 //Mutable Class
{
    public string Vorname { get; set; }
    public string Nachname { get; set; }
    public List<int> Numbers { get; } = new();
}

//[System.Runtime.CompilerServices.SkipLocalsInitAttribute]
public class Person2 //Immutable Class
{
    public Person2(string vorname, string nachname)
    {
        Vorname = vorname;
        Nachname = nachname;
    }

    //
    public void Juhu()
    {
        //int x;
        //Console.WriteLine(x);
    }

    public string Vorname { get; }
    public string Nachname { get; }
}