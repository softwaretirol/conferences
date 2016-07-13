using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NEtDayFranken
{
    static class ExtensionMethods
    {
        public static IEnumerable<T> Where<T>(this IEnumerable<T> s, Func<T, bool> where)
        {
            foreach (var element in s)
            {
                if (where(element))
                    yield return element;
            }
        }
    }

    class Program
    {
        //Action, Func
        public delegate void MyDelegate();

        static void Main(string[] args)
        {
            HashSet<Person> persons = new HashSet<Person>();
            Person lastPerson = null;
            for (int i = 0; i < 100; i++)
            {
                Person p = new Person
                {
                    Vorname = "Christian",
                    Nachname = "Giesswein"
                };
                persons.Add(p);
                lastPerson = p;
            }

            if (persons.Contains(lastPerson))
            {
                
            }

            //Console.WriteLine(p);
            Console.ReadLine();
            //var nr = new int[] {1, 2, 3, 4};
            //nr.Where(x => x < 3);


            //MyDelegate del = () => { };
           
            //List<Action> delegateList = new List<Action>();
            //for (int i = 0; i < 10; i++)
            //{
            //    int myI = i;
            //    delegateList.Add(() => Console.WriteLine(myI));
            //}

            //foreach (var action in delegateList)
            //{
            //    action();
            //}
            //MyMethod(() => delegateList.Count);
            //nameof(delegateList.Count)

            // yield

            //foreach (var x in 1)
            //{
                
            //}
            //int[] nr = {1, 2, 3};
            //var enumerator = nr.GetEnumerator();
            //while (enumerator.MoveNext())
            //{
            //    enumerator.Current;
            //}

            //var numbers = GenerateNumbers();
            //foreach (var nr in numbers)
            //{
                
            //}
            Console.ReadLine();
        }

        public static IEnumerable<int> GenerateNumbers()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(i);
                yield return i;
                yield return i;
                yield return i;
                yield return i;
                yield return i;
                yield return i;
            }
        }

        private static void MyMethod(Expression<Func<object>> myFunc)
        {
            var unaryExpression = (UnaryExpression)myFunc.Body;
            var member = (MemberExpression)unaryExpression.Operand;
            Console.WriteLine(member.Member.Name);

        }
        private static void IDisposableGC()
        {
            using (MyStopwach.Create())
            {
                for (int i = 0; i < 10; i++)
                {
                    using (var person = new Person())
                    {
                        person.Dispose();
                    }
                }
            }
            GC.Collect();
        }

        private static void DontTouchStringsInMemory()
        {
            var str = "Hello World";
            //var toUpper = new StringBuilder(str.Length);
            //for (int i = 0; i < str.Length; i++)
            //{
            //    toUpper.Append(char.ToUpper(str[i]));
            //}

            //fixed (char* strPtr = str)
            //{
            //    for (int i = 0; i < str.Length; i++)
            //    {
            //        strPtr[i] = char.ToUpper(strPtr[i]);
            //    }
            //}
            string str1 = "Hallo";
            string str2 = "Hallo";
            ToUpper(str1);
            str1.ToUpper();
            Console.WriteLine(str1);
            Console.WriteLine(str2);
        }

        private static void ToUpper(string str)
        {
            var method = typeof (string).GetMethod("FillStringChecked", BindingFlags.Static | BindingFlags.NonPublic);
            for (int i = 0; i < str.Length; i++)
            {
                method.Invoke(null, new object[] {str, i, char.ToUpper(str[i]).ToString()});
            }
        }
    }

    class MyStopwach : IDisposable
    {
        private Stopwatch watch;

        public MyStopwach()
        {
            watch =Stopwatch.StartNew();
        }

        public static MyStopwach Create()
        {
            return new MyStopwach();
        }
        public void Dispose()
        {
            Console.WriteLine(watch.Elapsed);
        }
    }

    [DebuggerDisplay("{Vorname,nq} {Nachname,nq}")]
    class Person : IDisposable
    {
        private int age = 42;
        public string Vorname { get; set; }
        public string Nachname { get; set; }

        public Person()
        {
            Console.WriteLine("CTOR");
        }

        ~Person()
        {
            Console.WriteLine("Destruktor");
        }

        public void Dispose()
        {
            Console.WriteLine("Dispose");
            GC.SuppressFinalize(this);
        }
    }
}
