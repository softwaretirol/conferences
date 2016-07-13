using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DWXEFWPF.DataAccess
{
    //public class PersonRepository
    //{
    //    public IEnumerable<Person> GetAllPErsons()
    //    {
            
    //    } 
    //}
    public class WpfDemoContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Person>().HasKey(x => x.Schlüssel);
        }
    }

    public class Person
    {
        //[Key]
        public int Id { get; set; }

        [StringLength(10)]
        public string Vorname { get; set; }
        public string Nachname { get; set; }

        public string Stadt { get; set; }

    }
}
