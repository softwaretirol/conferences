using System;
using Microsoft.CodeAnalysis;

namespace Generator
{
    [Generator]
    public class MyCodeGenerator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
        }

        public void Execute(GeneratorExecutionContext context)
        {
            //context.Compilation.GetSymbolsWithName();
            context.AddSource("Test", "namespace A { public class B{} }");
        }
    }
}
