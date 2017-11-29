using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Lambdas
{
    class Article
    {
        public int ID { get; set; }
    }

    class ArticleTable : IQueryable<Article>
    {
        public IEnumerator<Article> GetEnumerator()
        {
            throw new NotImplementedException();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Expression Expression { get; }
        public Type ElementType { get; }
        public IQueryProvider Provider { get; }
    }

    class Program
    {
        public static int Counter { get; set; }
        static Func<object> TestMethod(Expression<Func<object>> expression)
        {
            //Expression.Call()
            return expression.Compile();
        }

        public static void A()
        {
            
        }
        static void Main(string[] args)
        {
            var methodInfo = typeof(Program).GetMethod("A");
            var actionMethod = (Action)Expression.Lambda(Expression.Call(methodInfo)).Compile();


            // 0,3 mit Reflection
            var watch = Stopwatch.StartNew();
            for (int i = 0; i < 1000000; i++)
            {
                actionMethod();
                //methodInfo.Invoke(null, new object[0]);
            }
            Console.WriteLine(watch.Elapsed);
            Console.ReadLine();

            //new int[] {1,23}.AsQueryable().where
            var method = TestMethod(() => Counter);

            var counter1 = method();


           List<Action> actions = new List<Action>();
            for (int i = 0; i < 10; i++)
            {
                //var x = i;
                actions.Add(() => Console.WriteLine(i));
            }
            

            foreach (var action in actions)
                action();

            Console.ReadLine();
        }
    }
}
