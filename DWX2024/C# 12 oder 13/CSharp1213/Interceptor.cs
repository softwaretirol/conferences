using System.Runtime.CompilerServices;

namespace System.Runtime.CompilerServices
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    sealed class InterceptsLocationAttribute(string filePath, int line, int column) : Attribute
    {
    }
}




namespace CSharp1213
{
    static class Interceptor
    {
        [InterceptsLocation(@"C:\Users\cgiesswein\RiderProjects\DWX2024CSharp\CSharp1213\Program.cs", line: 3, column: 9)]
        public static void InterceptConsole(string message)
        {
            Console.WriteLine($"Intercepted Juhu");
        }
    }
}