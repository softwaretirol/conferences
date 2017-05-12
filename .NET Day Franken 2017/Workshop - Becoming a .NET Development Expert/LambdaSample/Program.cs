using System;
using System.Collections.Generic;
using System.Linq;

namespace LambdaSample
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            List<Action> actions  =new List<Action>();
            for (int i = 0; i < 10; i++)
            {
                int y = i;
                actions.Add(() => Console.WriteLine(y));
            }
            //foreach (var x in Enumerable.Range(0, 10))
            //{
            //    actions.Add(() => Console.WriteLine(x));
            //}

            foreach (var action in actions)
                action();

            Console.ReadLine();


            //string str = "Hallo";
            //Action myAction = () => Console.WriteLine(str);

            //str = "Welt";
            //myAction();
            Console.ReadLine();
        }
    }
}