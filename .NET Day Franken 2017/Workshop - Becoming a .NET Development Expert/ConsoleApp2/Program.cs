using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp2.Annotations;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            SayHello();
        }

        static void SayHello([CallerMemberName] string member = "")
        {
            Console.WriteLine("Hello!");
        }


    }


    class BindableBase : INotifyPropertyChanged
    {
        private string name;
        public event PropertyChangedEventHandler PropertyChanged;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged(Super<BindableBase>(x => x.Name));
            }
        }
        static string Super<T>(Expression<Func<T, object>> func)
        {
            var memberExpression = func.Body as MemberExpression;
            return memberExpression.Member.Name;
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
