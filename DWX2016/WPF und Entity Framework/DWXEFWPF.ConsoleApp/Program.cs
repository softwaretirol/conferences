using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DWXEFWPF.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new WpfDemoContext())
            {
                db.Persons.Add(new Person()
                {
                    Vorname = "Christian",
                    Nachname = "Giesswein"
                });
                db.SaveChanges();
            }
        }
    }


}
