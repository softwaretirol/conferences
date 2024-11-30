

System.Threading.Lock newLock = new();
lock (newLock)
{
    
}

object lockObject = new object();
lock (lockObject)
{
    
}

MyMethod("A");
MyMethod("A", "B");
MyMethod("A", "B", "C");

Console.WriteLine("Hello, World!");

void MyMethod(params ReadOnlySpan<string> args)
{
    
}

public class Person
{
    public string Name
    {
        get => field;
        set => field = value;
    }
}