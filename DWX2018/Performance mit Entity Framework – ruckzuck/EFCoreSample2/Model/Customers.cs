using System;
using System.Collections.Generic;

namespace EFCoreSample2.Model
{
    public partial class Customers
    {
        public Customers()
        {
            Orders = new HashSet<Orders>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Orders> Orders { get; set; }
    }
}
