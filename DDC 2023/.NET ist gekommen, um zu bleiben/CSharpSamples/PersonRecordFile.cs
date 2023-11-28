using System.Text;

namespace CSharpSamples;

/// <summary>
/// Test Record
/// </summary>
/// <param name="FirstName">Vorname</param>
record PersonRecordFile(
    string FirstName);


public class TestClass
{
    public static async void Test()
    {
        await Task.Delay(1000);
        var linq = new[] { 1, 2, 3 }.Where(x => x > 1).ToList(); 
    }

    public TestClass()
    {
        var buffer = new byte[] { 1, 2, 3, 4, 5 };
        var span = buffer.AsSpan(0, 3);
        buffer[0] = 42;
        var text = Encoding.UTF8.GetString(span);

        if(buffer is [1, 2, .., 8])
        {

        }

        var s5 = "Hallo Welt"u8;
        var data = Encoding.UTF8.GetBytes("Hallo Welt");
    }
}