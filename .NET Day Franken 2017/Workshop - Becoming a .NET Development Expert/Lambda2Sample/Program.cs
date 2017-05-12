using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Lambda2Sample
{
    class Person
    {
        public string Name { get; set; }
    }
    class Program
    {
        public string FirstName { get; set; }
        static void Main(string[] args)
        {
            // () => new Person();
            var newExpression = Expression.New(typeof(Person).GetConstructors()[0]);
            var lambdaExpression = Expression.Lambda<Func<object>>(newExpression);
            Func<object> func = lambdaExpression.Compile();

            var watch = Stopwatch.StartNew();
            for (int i = 0; i < 10000000; i++)
            {
                //Person p = new Person();
                //Person p = Activator.CreateInstance<Person>();
                func();

            }
            Console.WriteLine(watch.Elapsed);


            //dbContext.Persons.Where(x => x.Name.Contains("abc"))
            //string propertyName = Super<Program>(p => p.FirstName);
        }

        //Action / Func = delegate --> Methode muss generiert werden!
        //Expression --> Die Information über die LambdaExpression
        static string Super<T>(Expression<Func<T, object>> func)
        {
            var memberExpression = func.Body as MemberExpression;
            return memberExpression.Member.Name;
        }
    }

    //class Trace
    //{
    //    public void LogMessage(string message)
    //    {

    //    }
    //    public void LogMessage(Func<string> message)
    //    {
    //        message();
    //    }
    //}

    //class ViewModel
    //{
    //    private readonly Func<string> title;

    //    public string Title
    //    {
    //        get { return title(); }
    //    }

    //    public ViewModel(Func<string> title)
    //    {
    //        this.title = title;
    //    }
    //}
}
