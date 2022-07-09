// See https://aka.ms/new-console-template for more information


using System.Numerics;

Person p = new()
{
    //LastName = "Giesswein"
};


if(p == null)
{

}

if(p is null)
{

}

if(p is not null)
{

}
if(p is { })
{

}

if(p is { FirstName: "Christian" })
{

}

Console.WriteLine("Hello, World!");

//string s = $$$$"Hallo {{{DateTime.Now:d}}} Welt {} ";
string s = $$$"""
 
 Hallo {{{DateTime.Now:d}}}
 
 
 """;
Console.WriteLine(s);

public interface ISample
{
    public abstract static ISample ParseString(string input);
    public void Juhu()
    {

    }
}

public class NumericUpDown<T>
    where T : INumber<T>
{
    public T CurrentValue { get; set; }

    public void Increment()
    {
        CurrentValue = CurrentValue + CurrentValue;
    }
}

[My<string>]
public class Demo
{

}

public class MyAttribute<T> : Attribute
{

}