using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Lifetime;

namespace IOCSample
{
    class Program
    {
        static void Main(string[] args)
        {
            // System.ComponentModel.Composition - MEF
            // [Export] = "Register"
            // [ImportingConstructor] --> Abhängigkeiten für Constructor
            // [Import] --> Dependency Injection per Property/Field/Method
            // var container = new CompositionContainer(new AssemblyCatalog(typeof(Program).Assembly));
            // var container = new CompositionContainer(new AssemblyCatalog(typeof(Program).Assembly));//new DirectoryCatalog(Directory.GetCurrentDirectory())));
            // ICalculator calculator = container.GetExportedValue<ICalculator>();
            // var areEqual = container.GetExportedValue<ILogger>() == container.GetExportedValue<ILogger>();


            // Unity per NuGet = Paket "Unity"
            var container = new UnityContainer();
            container.RegisterType<ICalculator, Calculator>();
            container.RegisterType<ILogger, Logger>();//new ContainerControlledLifetimeManager());
            var areEqual = container.Resolve<ILogger>() == container.Resolve<ILogger>();
            var calculator = container.Resolve<ICalculator>();

            //var container = new Container();
            //container.Register(typeof(ICalculator), typeof(Calculator));
            //container.Register(typeof(ILogger), typeof(Logger));
            //ICalculator calculator = (ICalculator)container.Resolve(typeof(ICalculator));


            int idx = 1;
            foreach (var operation in calculator.Operations)
            {
                Console.WriteLine($"{idx++}) {operation.Name}");
            }

            int operationIndex = int.Parse(Console.ReadLine());
            double x = double.Parse(Console.ReadLine());
            double y = double.Parse(Console.ReadLine());

            var result = calculator.Operations.ElementAt(operationIndex - 1).Calculate(x, y);
            Console.WriteLine(result);
            Console.ReadLine();
        }
    }

    class Container
    {
        private Dictionary<Type, Type> mappedTypes = new Dictionary<Type, Type>();
        public void Register(Type @interface, Type implementation)
        {
            mappedTypes[@interface] = implementation;
        }

        public object Resolve(Type @interface)
        {
            var targetImplementation = mappedTypes[@interface];
            var ctorInformation = targetImplementation.GetConstructors()[0];
            var parameters = ctorInformation.GetParameters();
            List<object> parameterInstances = new List<object>();
            foreach (var parameter in parameters)
            {
                parameterInstances.Add(Resolve(parameter.ParameterType));
            }
            return Activator.CreateInstance(targetImplementation, parameterInstances.ToArray());
        }
    }

    class AddOperation : ICalcOperation
    {
        public string Name => "Addiere";
        public double Calculate(double x, double y)
        {
            return x + y;
        }
    }
    class SubOperation : ICalcOperation
    {
        public string Name => "Subtrahiere";
        public double Calculate(double x, double y)
        {
            return x - y;
        }
    }

    interface ICalculator
    {
        IEnumerable<ICalcOperation> Operations { get; }
    }

    interface ILogger
    {
        void Log(string message);
    }

    [Export(typeof(ILogger))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    class Logger : ILogger
    {
        //[Import]
        //private ICalculator calculator;

        //[Dependency]
        //[Import]
        // Dependency Injection via Property - Property Injection
        //public ICalculator Calculator { get; set; }

        //[Import] [Dependency]
        //public void Initialize(ICalculator calculator)
        //{
            
        //}

        //public Logger(ICalculator)
        //{
            
        //}
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }

    [Export(typeof(ICalculator))]
    class Calculator : ICalculator
    {
        private readonly ICalcOperation[] operations = new ICalcOperation[]
        {
            new AddOperation(), 
            new SubOperation(), 
        };

        private readonly ILogger logger;

        [ImportingConstructor]
        public Calculator(ILogger logger) //Dependency Injection via Constructor
        {
            this.logger = logger;
            logger.Log("Calculator .ctor");
        }

        public IEnumerable<ICalcOperation> Operations => operations;
    }

    interface ICalcOperation
    {
        string Name { get; }
        double Calculate(double x, double y);
    }
}
